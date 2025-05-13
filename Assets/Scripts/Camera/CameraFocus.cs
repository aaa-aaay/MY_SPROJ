using Unity.Cinemachine;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{

    public CinemachineBrain Brain;
    public ICinemachineCamera CamA;
    public ICinemachineCamera CamB;


    void Start()
    {
        CamA = GetComponent<CinemachineCamera>();
        CamB = GetComponent<CinemachineCamera>();

        //int layer = 1; //The layer to override
        //int priority = 1; //The priority of the override;
        //float weight = 1f; //wieght of new camera
        //float blemdTime = 0f; //The time it takes to blend between cameras
        //Brain.SetCameraOverride(layer, priority, CamA, CamB, weight, blemdTime);
    }


}
