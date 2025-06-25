using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private EnemyStats stats;
    [SerializeField] private Transform stateColorDebug;
    public Animator animator;


    [HideInInspector] public float timer;

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

        timer += Time.deltaTime;



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
            timer = 0;


        }
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
}
