using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerAbilities : MonoBehaviour
{
    protected PlayerController player;
    protected InputAction abilityAction1; 
    [HideInInspector]public InputAction abilityAction2; 
    protected InputAction abilityAction3; 
    [SerializeField]private int playerFor;
    private void Start()
    {
        
    }
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public virtual void SetAbilityToPlayer()
    {
        GameObject playerMovementOBJ;
        if (playerFor == 1) {
            playerMovementOBJ = GameObject.Find("Player1 Movement");

        }
        else
        {
            playerMovementOBJ = GameObject.Find("Player2 Movement");
        }
        gameObject.transform.SetParent(playerMovementOBJ.transform, false);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;

        player = gameObject.GetComponentInParent<PlayerController>();
        abilityAction1 = player.abilityAction;
        abilityAction2 = player.abilityAction2;
        abilityAction3 = player.abilityAction3;
        gameObject.SetActive(true);

    }
}
