using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    private Camera P1camera;
    private Camera P2camera;
    private CinemachinePositionComposer p1Cinemachine;
    private CinemachinePositionComposer p2Cinemachine;
    private Camera singleCam;
    private bool isSide = false;
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
        DontDestroyOnLoad(gameObject); // Optional: persists between scenes


        singleCam = gameObject.GetComponentInChildren<Camera>();
    }

    public void SetPlayerCameras(Camera cam, int player)
    {
        //maybe set cinemachine next time;
        if(player == 1)
        {
            P1camera = cam;
            p1Cinemachine = P1camera.transform.parent.GetComponentInChildren<CinemachinePositionComposer>();
        }
        if (player == 2) {
            P2camera = cam;
            p2Cinemachine = P2camera.transform.parent.GetComponentInChildren<CinemachinePositionComposer>();
        }
    }

    public void InitCameras()
    {
        SideSide();
    }

    public void SwitchMode(mode mode)
    {
        if (mode == mode.Single) { }
        else if (mode == mode.SideSide) { UpDown(); }
        else if(mode == mode.UpDown) { SideSide();}
        // Implement switching logic here (e.g., single-player to split-screen)
    }
    public void SwitchModeButton()
    {
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
        P1camera.rect = new Rect(0f, 0f, 0.5f, 1f); 
        P2camera.rect = new Rect(0.5f, 0f, 0.5f, 1f);

        p1Cinemachine.CameraDistance = topDownCameraDistance;
        p2Cinemachine.CameraDistance = topDownCameraDistance;

        sidesidePanel.SetActive(true);
        updownPanel.SetActive(false);
        isSide = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)) {
            isSide = true;


        }
    }
}
