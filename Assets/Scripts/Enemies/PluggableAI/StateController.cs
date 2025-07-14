using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private EnemyStats stats;
    [SerializeField] private Transform stateColorDebug;
    public Animator animator;
    [HideInInspector] public Collider2D hitColldier;


    [HideInInspector] public float timer;
    [HideInInspector] public float timer2;

    public State currentState;
    public State remainState;
    [HideInInspector] public bool readyToGoNextState;
    public Action[] alwaysActiveActions;  //actions that are always happening "e.g. maybe follow the player no matter the state?"

    private bool isDead;
    private void Start()
    {
        readyToGoNextState = false;
    }

    void Update()
    {
        if (isDead) return;


        //updating all states and actions
        foreach (Action action in alwaysActiveActions)
        {
            action.Act(this);
        }
        currentState.UpdateState(this);

    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState) {
            currentState = nextState;
            readyToGoNextState = false;
            currentState.InitActions(this);
            timer = 0;
            timer2 = 0;




        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hitColldier = collision;
    }


    //debugging gismos
    private void OnDrawGizmos()
    {
        if(currentState != null && stateColorDebug != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(stateColorDebug.position, 5.0f);
        }
    }

    //useful timer for actions
    public bool CheckIfCountDownElpased(float duration)
    {
        timer += Time.deltaTime;
        return (timer >= duration);
    }

    public bool CheckIfCountDownElpasedSecond(float duration)
    {
        timer2 += Time.deltaTime;
        return (timer2 >= duration);
    }
}
