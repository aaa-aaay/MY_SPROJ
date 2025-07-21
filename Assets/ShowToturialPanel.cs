using UnityEngine;

public class ShowToturialPanel : MonoBehaviour
{
    [SerializeField] GameObject panel1;
    [SerializeField] GameObject panel2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        panel1.SetActive(false);
        panel1.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player.playerNo == 1)
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
