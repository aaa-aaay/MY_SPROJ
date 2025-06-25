using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Golem/GoldemIdle")]
public class GolemIdleAction : Action
{

    public override void Act(StateController controller)
    {
        Idle(controller);
        controller.animator.Play("idle");
    }

    private void Idle(StateController controller)
    {

        Debug.Log("Golem In Idle");

    }
}
