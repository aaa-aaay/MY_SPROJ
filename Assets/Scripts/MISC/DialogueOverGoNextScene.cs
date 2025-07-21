using UnityEngine;

public class DialogueOverGoNextScene : MonoBehaviour
{
    [SerializeField] private OnTriggerDialouge dialouge;

    [SerializeField] private ShowStoryAtStart startDialouge;
    [SerializeField] private int nextSceneNo;

    [SerializeField] private bool deleteManagers = false;
    [SerializeField] private DestoryManagers destoryManager;

    private bool wentNextScene;

    private void Start()
    {
        wentNextScene = false;
    }
    private void Update()
    {
        if (dialouge != null && dialouge.dialougeFinished)
        {
            if (!wentNextScene)
            {
                MySceneManager.Instance.GoNextScene(nextSceneNo);
                wentNextScene = true;
            }

        }

        if (startDialouge != null && startDialouge.dialougeFinished)
        {
            if (!wentNextScene)
            {
                if (deleteManagers)
                {
                    destoryManager.DestorySingletonManagers();
                }

                
                MySceneManager.Instance.GoNextScene(nextSceneNo);
                wentNextScene = true;
            }

        }

    }
}
