using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class BossSceneManager : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] CinemachineCamera cinemachineCamera;

    [SerializeField] PlayerAbilities ability1;
    [SerializeField] PlayerAbilities ability2;

    [SerializeField] healthBar healthBar;

    [SerializeField] GameObject boss;
    [SerializeField] private float startingPositionBoss;
    [SerializeField] private float restingPositionBoss;
    [SerializeField] private float moveSpeed;

    [SerializeField] GameObject head;

    [SerializeField] private int nextSceneNo;


    float lerpDuration = 1f; // time in seconds
    float lerpTime = 0f;

    private bool sceneStarted;


    private void Start()
    {
        healthBar.gameObject.SetActive(false);
        CameraManager.Instance.LockCam(false);
        CameraManager.Instance.SetMainCam(_camera, cinemachineCamera);
        CameraManager.Instance.SwitchMode(CameraManager.mode.Single,null);
        PPManager.Instance.SetMainCameraVolume(_camera.gameObject.GetComponent<Volume>());


        ability1.SetAbilityToPlayer();
        ability2.SetAbilityToPlayer();

        sceneStarted = true;

    }


    private void Update()
    {
        if (sceneStarted) {

            if (lerpTime < lerpDuration)
            {
                lerpTime += Time.deltaTime;
                float t = lerpTime / lerpDuration;

                float y = Mathf.Lerp(startingPositionBoss, restingPositionBoss, t);
                boss.transform.position = new Vector3(boss.transform.position.x, y, boss.transform.position.z);
            }

            if (Mathf.Abs(boss.transform.position.y - restingPositionBoss) < 0.01f)
            {
                sceneStarted = false;

                healthBar.gameObject.SetActive(true);

            }
        }


        if (!sceneStarted) {



            if(head == null)
            {
                MySceneManager.Instance.GoNextScene(nextSceneNo);
            }
            


        
        
        
        
        }



    }
}
