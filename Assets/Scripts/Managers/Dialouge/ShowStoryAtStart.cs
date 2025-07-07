using TMPro;
using UnityEngine;



[DefaultExecutionOrder(10)]
public class ShowStoryAtStart : MonoBehaviour
{

    [SerializeField] Dialogue dialouge;
    [SerializeField] TMP_Text displayText;
    [SerializeField] GameObject panel;
    [SerializeField] string BGMNameForScene;


    private bool dialougeStarted;
    public bool dialougeFinished;


    private void Start()
    {
        DialogueManager.Instance.SetDisplayFields(false, null,displayText, panel);
        DialogueManager.Instance.DisplayStoryDialouge(dialouge);

        PlayerManager.Instance.GetPlayer1().FreezePlayer(true);
        PlayerManager.Instance.GetPlayer2().FreezePlayer(true);

        dialougeStarted = true;
    }



    private void Update()
    {
        if (dialougeStarted)
        {
            if (!DialogueManager.Instance.StillHaveDialogue())
            {
                PlayerManager.Instance.GetPlayer1().FreezePlayer(false);
                PlayerManager.Instance.GetPlayer2().FreezePlayer(false);
                dialougeStarted = false;
                dialougeFinished = true;
                AudioManager.instance.PlayBackgroundMusic(BGMNameForScene);
                return;
            }
            if (PlayerManager.Instance.GetPlayer1().interactAction.WasPressedThisFrame() || PlayerManager.Instance.GetPlayer2().interactAction.WasPressedThisFrame())
            {

                DialogueManager.Instance.DisplayNextSentence(true);

            }
        }
    }
    //set the text box to new text box
    //set teh other things to null
    //show at start of scene
}
