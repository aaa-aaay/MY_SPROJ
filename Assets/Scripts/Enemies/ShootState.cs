using UnityEngine;

public class ShootState : IState
{
    private StateMachine stateMachine;
    private GameObject bulletPrefab;
    private GameObject owner;
    private Animator animator;
    private IState nextState;
    private float timer = 0f;

    public ShootState(GameObject bulletPrefab, StateMachine stateMachine, GameObject owner, IState nextState, Animator ani = null)
    {
        this.bulletPrefab = bulletPrefab;


        this.stateMachine = stateMachine;
        this.owner = owner;
        animator = ani;
        this.nextState = nextState;

        //GameObject bullet = GameObject.Instantiate(bulletPrefab, owner.transform.position, Quaternion.identity);
    }

    public void OnEnter()
    {
        timer = 0;
        if (animator != null)
        animator.Play("shoot");
        //shoot the bullet
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;
        if(timer > 1.0f)
        {
            stateMachine.ChangeState(nextState);
        }
    }
}
