using UnityEngine;

public class GiveAbilitiesNPC : MonoBehaviour
{

    bool abilityGiven;
    [SerializeField] PlayerAbilities ability1;
    [SerializeField] PlayerAbilities ability2;
    [SerializeField] GameObject panel1;
    [SerializeField] GameObject panel2;


    [SerializeField] TutorialPlayer toturial;
    private OnTriggerDialouge dialouge;

    private void Start()
    {
        abilityGiven = false;
        dialouge = GetComponent<OnTriggerDialouge>();
    }

    private void Update()
    {
        if (!abilityGiven && dialouge.dialougeFinished)
        {
            ability1.SetAbilityToPlayer();
            ability2.SetAbilityToPlayer();
            abilityGiven = true;


            if (toturial != null)
            {

                toturial.ShowToturial();



            }

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!abilityGiven) return;
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if(player.playerNo == 1)
            {
                panel1.SetActive(true);
            }
            else
            {
                panel2.SetActive(true);

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!abilityGiven) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player.playerNo == 1)
            {
                panel1.SetActive(false);
            }
            else
            {
                panel2.SetActive(false);

            }
        }
    }


}
