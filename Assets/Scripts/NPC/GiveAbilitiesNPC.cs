using UnityEngine;

public class GiveAbilitiesNPC : MonoBehaviour
{

    bool abilityGiven;
    [SerializeField] PlayerAbilities ability1;
    [SerializeField] PlayerAbilities ability2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ability1.SetAbilityToPlayer();
            ability2.SetAbilityToPlayer();
        }
    }
}
