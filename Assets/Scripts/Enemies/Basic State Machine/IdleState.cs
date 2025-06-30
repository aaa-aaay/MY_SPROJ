using UnityEngine;

public class IdleState : IState
{

    private Animator animator;

    public IdleState(Animator ani)
    {
        animator = ani;
    }

    public void OnEnter()
    {
        animator.Play("idle");
        //play animation
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}
