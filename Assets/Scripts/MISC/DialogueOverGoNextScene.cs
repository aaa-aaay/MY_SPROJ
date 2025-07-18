using UnityEngine;

public class DialogueOverGoNextScene : MonoBehaviour
{
    [SerializeField] private OnTriggerDialouge dialouge;
    [SerializeField] private int nextSceneNo;

    private bool wentNextScene;

    private void Start()
    {
        wentNextScene = false;
    }
    private void Update()
    {
        if (dialouge.dialougeFinished)
        {
            if (!wentNextScene)
            {
                MySceneManager.Instance.GoNextScene(nextSceneNo);
                wentNextScene = true;
            }

        }
    }
}
