using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Golem/GolemLaser")]
public class GoelmLaserAction : Action
{
    private GolemData data;
    public override void Act(StateController controller)
    {
        data = controller.gameObject.GetComponent<GolemData>();
        if (!controller.animator.GetCurrentAnimatorStateInfo(0).IsName("laser_cast"))
        {
            if(data.LaserFinished == true)
            {
                controller.animator.Play("laser_cast");
                data.laser.StartLaser();
            }

        }
        CheckAnimationFinished(controller);

    }

    public override void Init(StateController controller)
    {

    }

    private void CheckAnimationFinished(StateController controller)
    {
        if (data.LaserFinished)
        {
            Debug.Log("Laser stopped");
            data.LaserFinished = true;
            controller.readyToGoNextState = true;

        }




    }
}
