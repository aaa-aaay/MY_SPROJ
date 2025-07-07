using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialouge; 


    public void TriggerDialouge()
    {
        DialogueManager.Instance.StartDialogue( dialouge );


        PlayerManager.Instance.GetPlayer1().FreezePlayer(true);
        PlayerManager.Instance.GetPlayer2().FreezePlayer(true);
    }

    public void NextDialouge()
    {


        DialogueManager.Instance.DisplayNextSentence();
        //DialogueManager.Instance.
    }

    public bool CheckIfHaveDialouge()
    {

        if(DialogueManager.Instance.StillHaveDialogue()) return true;
        else
        {
            PlayerManager.Instance.GetPlayer1().FreezePlayer(false);
            PlayerManager.Instance.GetPlayer2().FreezePlayer(false);
            return false;
        }
    }
}
