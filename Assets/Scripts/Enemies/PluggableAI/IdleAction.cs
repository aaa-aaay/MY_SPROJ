using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class IdleAction : Action
{
    public override void Act(StateController controller)
    {
        Idle(controller);
        controller.animator.Play("idle");
    }

    private void Idle(StateController controller)
    {

        
        Debug.Log("Golem Entered Idle");

    }
}
