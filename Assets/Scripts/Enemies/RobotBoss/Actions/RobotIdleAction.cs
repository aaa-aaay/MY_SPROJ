using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Robot/idle")]
public class RobotIdleAction : Action
{

    private Animator leftHandAnimator;
    private Animator rightHandAnimator;

    public override void Act(StateController controller)
    {

        leftHandAnimator = controller.GetComponent<RobotBoss>().leftHandanimater;
        rightHandAnimator = controller.GetComponent<RobotBoss>().rightHandAnimator;

        controller.animator.Play("idle");
        leftHandAnimator.Play("idle");
        rightHandAnimator.Play("idle");


    }

    public override void Init(StateController controller)
    {

    }
}
