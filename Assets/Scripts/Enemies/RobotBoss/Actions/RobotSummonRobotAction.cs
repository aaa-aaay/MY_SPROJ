using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Actions/Robot/SummonRobotRight")]
public class RobotSummonRobotAction : Action
{

    private Animator rightHandAnimator;
    private RobotBoss boss;
    public override void Act(StateController controller)
    {

        Vector3 currentPos = boss.rightHand.transform.position;
        Vector3 targetPos = boss.rightHandSwipePosition.position;


        if (!boss.rightRobotSummoned)
        {
            // Only move if not already close
            if (Vector3.Distance(currentPos, targetPos) > 0.05f)
            {

                boss.rightHand.transform.position = Vector3.Lerp(
                boss.rightHand.transform.position,
                boss.rightHandSwipePosition.position,
                Time.deltaTime * boss.speedToGoAttackPosition);
            }
            else
            {

                Instantiate(boss.blueRobot, boss.rightHand.transform.position, Quaternion.identity);
                boss.rightRobotSummoned = true;

            }
        }

        else
        {

            boss.rightHand.transform.position = Vector3.Lerp(
            boss.rightHand.transform.position,
            boss.rightHandRestingPosition.position,
            Time.deltaTime * boss.speedToGoAttackPosition);

            if (Vector3.Distance(boss.rightHand.transform.position, boss.rightHandRestingPosition.position) < 0.05f)
            {
                boss.rightRobotSummoned = false;
                controller.readyToGoNextState = true;


            }
        }

    }

    public override void Init(StateController controller)
    {
        boss = controller.GetComponent<RobotBoss>();
        rightHandAnimator = controller.GetComponent<RobotBoss>().rightHandAnimator;

    }
}
