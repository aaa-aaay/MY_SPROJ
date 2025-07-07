using UnityEngine;

public class DialougeOnInteract : Interactable
{

    DialogueTrigger dialougeTrigger;

    private bool DialougeStarted;

    protected override void Start()
    {
        dialougeTrigger = GetComponent<DialogueTrigger>();
        base.Start();


    }
    protected override void OnInteract()
    {
        dialougeTrigger.TriggerDialouge();
        DialougeStarted= true;
        interacting = true;


    }

    protected override void Interacting()
    {
        if (!DialougeStarted) return;


        for (int i = 0; i < playersInRange.Count; i++)
        {
            PlayerController player = playersInRange[i];

            if (dialougeTrigger.CheckIfHaveDialouge() && player.interactAction.WasPressedThisFrame())
            {
                dialougeTrigger.NextDialouge();
            }
        }

        if (!dialougeTrigger.CheckIfHaveDialouge())
        {
            DialougeStarted = false;
            interacting =false;
        }
    }
}
