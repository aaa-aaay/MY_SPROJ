using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Robot/SwipeAttackLeft")]
public class RobootSwipeAttackLeft :Action
{
    private Animator leftHandAnimator;
    private RobotBoss boss;
    public override void Act(StateController controller)
    {



        Vector3 currentPos = boss.leftHand.transform.position;
        Vector3 targetPos = boss.leftHandSwipePosition.position;


        if (!boss.leftHandHasSwiped)
        {
            boss.leftHandanimater.Play("TurnToFist");
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
                boss.leftHandRigidbody.AddForce(Vector2.left * boss.swipeAttackSpeed, ForceMode2D.Impulse);
                Debug.Log("this is called");
                boss.leftHandHasSwiped = true;

            }
        }

        else
        {
            if (!controller.CheckIfCountDownElpasedSecond(1)) return;
            if (boss.leftHandRigidbody.linearVelocity.magnitude < 0.1f)
            {

                boss.leftHandanimater.Play("TurnToPalm");

                boss.leftHand.transform.position = Vector3.Lerp(
                boss.leftHand.transform.position,
                boss.leftHandRestingPosition.position,
                Time.deltaTime * boss.speedToGoAttackPosition);
            }


            if (Vector3.Distance(boss.leftHand.transform.position, boss.leftHandRestingPosition.position) < 0.05f)
            {
                boss.leftHandHasSwiped = false;
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
