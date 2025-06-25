using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Actions/Shoot")]
public class ShootAction : Action
{

    public override void Act(StateController controller)
    {
        Animator animator = controller.animator;

        // Only trigger once
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("shoot"))
        {
            animator.Play("shoot");
        }

        CheckAnimationFinished(controller);
    }

    private void CheckAnimationFinished(StateController controller)
    {
        Animator animator = controller.animator;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("shoot") && stateInfo.normalizedTime >= 1f)
        {
            controller.readyToGoNextState = true;
        }
    }
}
