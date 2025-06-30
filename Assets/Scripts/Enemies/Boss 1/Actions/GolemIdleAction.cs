using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Golem/GoldemIdle")]
public class GolemIdleAction : Action
{

    public override void Act(StateController controller)
    {
        Idle(controller);
        controller.animator.Play("idle");
    }

    public override void Init(StateController controller)
    {

    }

    private void Idle(StateController controller)
    {

        Debug.Log("Golem In Idle");

    }
}
