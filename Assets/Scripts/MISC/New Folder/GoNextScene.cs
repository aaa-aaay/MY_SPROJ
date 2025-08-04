using UnityEngine;

public class GoNextScene : MonoBehaviour
{
    public void GoScene(int sceneNo)
    {
        MySceneManager.Instance.GoNextScene(sceneNo);
    }
}
