using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;


[DefaultExecutionOrder(-6)]
public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    private Camera P1camera;
    private Camera P2camera;
    private CinemachinePositionComposer p1Cinemachine;
    private CinemachinePositionComposer p2Cinemachine;
    private Camera singleCam;
    private CinemachineCamera cm;
    private bool isSide = false;
    private bool cameraLock;
    [SerializeField][Range(1f, 20f)] private float sideCameraDistance;
    [SerializeField][Range(1f, 20f)] private float topDownCameraDistance;


    [SerializeField] private GameObject sidesidePanel;
    [SerializeField] private GameObject updownPanel;

    public enum mode {

        Single,
        SideSide,
        UpDown
    
    }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: persists between scenesCameraLock


        singleCam = gameObject.GetComponentInChildren<Camera>();
        Volume vol = singleCam.gameObject.GetComponent<Volume>();
        PPManager.Instance.SetMainCameraVolume(vol);
        cm = gameObject.GetComponentInChildren<CinemachineCamera>();
        singleCam.gameObject.SetActive(false);

        cameraLock = false;
    }

    public void SetPlayerCameras(Camera cam, int player)
    {
        //maybe set cinemachine next time;
        if(player == 1)
        {
            P1camera = cam;
            p1Cinemachine = P1camera.transform.parent.GetComponentInChildren<CinemachinePositionComposer>();
            Debug.Log("debug 1");
        }
        if (player == 2) {
            P2camera = cam;
            p2Cinemachine = P2camera.transform.parent.GetComponentInChildren<CinemachinePositionComposer>();
            Debug.Log("debug 2");
        }
    }

    public void InitCameras()
    {
        SideSide();
    }

    public void SwitchMode(mode mode, GameObject targetforSingle = null, Vector2 targetOffset = default)
    {
        if (cameraLock) return;
        if (mode == mode.Single) {

            MainCam(targetforSingle, targetOffset);


        }
        else if (mode == mode.SideSide) { UpDown(); }
        else if(mode == mode.UpDown) { SideSide();}
        // Implement switching logic here (e.g., single-player to split-screen)
    }
    public void SwitchModeButton()
    {
        if (cameraLock) return;
        if (isSide)
        {
            UpDown();
        }
        else
        {
            SideSide();
        }
    }

    private void SideSide()
    {
        DisablePlayerCamera(false);

        P1camera.rect = new Rect(0f, 0.5f, 1f, 0.5f);
        P2camera.rect = new Rect(0f, 0f, 1f, 0.5f);

        p1Cinemachine.CameraDistance = sideCameraDistance;
        p2Cinemachine.CameraDistance = sideCameraDistance;
        sidesidePanel.SetActive(false);
        updownPanel.SetActive(true);
        isSide = true;
    }
    private void UpDown()
    {
        DisablePlayerCamera(false);
        P1camera.rect = new Rect(0f, 0f, 0.5f, 1f); 
        P2camera.rect = new Rect(0.5f, 0f, 0.5f, 1f);

        p1Cinemachine.CameraDistance = topDownCameraDistance;
        p2Cinemachine.CameraDistance = topDownCameraDistance;

        sidesidePanel.SetActive(true);
        updownPanel.SetActive(false);
        isSide = false;
    }

    public void SetMainCam(Camera mainCam, CinemachineCamera cm)
    {
        singleCam = mainCam;
        Volume vol = singleCam.gameObject.GetComponent<Volume>();
        PPManager.Instance.SetMainCameraVolume(vol);
        this.cm = cm;

    }

    private void MainCam( GameObject target, Vector2 targetOffset)
    {



        DisablePlayerCamera(true);

        LockCam(true);


        if (cm == null)
        {

        }
        if (cm != null)
        {
            if (target != null)
            cm.Follow = target.transform;
            CinemachinePositionComposer camPos = cm.gameObject.GetComponent<CinemachinePositionComposer>();
            if (camPos != null) 
            camPos.TargetOffset = new Vector3(targetOffset.x, targetOffset.y, 0);

        }

        sidesidePanel.SetActive(false);
        updownPanel.SetActive(false);


    }

    private void DisablePlayerCamera(bool disable)
    {
        P1camera.gameObject.SetActive(!disable);
        P2camera.gameObject.SetActive(!disable);
        p2Cinemachine.gameObject.SetActive(!disable);
        p1Cinemachine.gameObject.SetActive(!disable);
        if(singleCam != null)
        singleCam.gameObject.SetActive(disable);
        if(cm != null)  
        cm.gameObject.SetActive(disable);
    }

    public void LockCam(bool lockCam)
    {
        cameraLock = lockCam;
    }

    public Camera GetPlayerCamera(int playerNo)
    {
        if (playerNo == 1)
        {
            return P1camera;
        }
        else
        {
            return P2camera;
        }
       
    }
}
