using UnityEngine;

public class DestoryManagers : MonoBehaviour
{


    public void DestorySingletonManagers()
    {
        if(CameraManager.Instance != null)
        CameraManager.Instance.DestoryGameObject();

        if(PPManager.Instance != null)
        PPManager.Instance.DestoryGameObject();

        if(PlayerManager.Instance != null)
        PlayerManager.Instance.DestoryGameObject();

        if(PlayerSelectionManager.Instance != null)
        PlayerSelectionManager.Instance.DestoryGameObject();

        if (AudioManager.instance != null)
            AudioManager.instance.DestoryGameObject();
    }
}
