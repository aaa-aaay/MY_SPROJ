using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Robot/SummonRobotLeft")]
public class RobotSummonRobotActionLeft : Action
{

    private Animator leftHandAnimator;
    private RobotBoss boss;
    public override void Act(StateController controller)
    {

        Vector3 currentPos = boss.leftHand.transform.position;
        Vector3 targetPos = boss.leftHandSwipePosition.position;


        if (!boss.leftRobotSummoned)
        {
            // Only move if not already close
            if (Vector3.Distance(currentPos, targetPos) > 0.05f)
            {

                boss.leftHand.transform.position = Vector3.Lerp(
                boss.leftHand.transform.position,
                boss.leftHandSwipePosition.position,
                Time.deltaTime * boss.speedToGoAttackPosition);
            }
            else
            {

                Instantiate(boss.redRobot, boss.leftHand.transform.position, Quaternion.identity);
                boss.leftRobotSummoned = true;

            }
        }

        else
        {

            boss.leftHand.transform.position = Vector3.Lerp(
            boss.leftHand.transform.position,
            boss.leftHandRestingPosition.position,
            Time.deltaTime * boss.speedToGoAttackPosition);

            if (Vector3.Distance(boss.leftHand.transform.position, boss.leftHandRestingPosition.position) < 0.05f)
            {

                boss.leftRobotSummoned = false;
                controller.readyToGoNextState = true;
            }
        }

    }

    public override void Init(StateController controller)
    {
        boss = controller.GetComponent<RobotBoss>();
        leftHandAnimator = controller.GetComponent<RobotBoss>().leftHandanimater;

    }
}
