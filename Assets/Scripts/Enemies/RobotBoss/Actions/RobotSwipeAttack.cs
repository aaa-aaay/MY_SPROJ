using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Robot/SwipeAttack")]
public class RobotSwipeAttack : Action
{

    private Animator leftHandAnimator;
    private Animator rightHandAnimator;
    private RobotBoss boss;
    public override void Act(StateController controller)
    {



        Vector3 currentPos = boss.rightHand.transform.position;
        Vector3 targetPos = boss.rightHandSwipePosition.position;


        if (!boss.rightHandHasSwiped)
        {
            boss.rightHandAnimator.Play("TurnToFist");
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
                boss.rightHandRigidbody.AddForce(Vector2.right * boss.swipeAttackSpeed, ForceMode2D.Impulse);
                Debug.Log("this is called");
                boss.rightHandHasSwiped = true;

            }
        }

        else
        {
            if (!controller.CheckIfCountDownElpased(1)) return;
            if (boss.rightHandRigidbody.linearVelocity.magnitude < 0.1f)
            {
                boss.rightHandAnimator.Play("TurnToPalm");

                boss.rightHand.transform.position = Vector3.Lerp(
                boss.rightHand.transform.position,
                boss.rightHandRestingPosition.position,
                Time.deltaTime * boss.speedToGoAttackPosition);
            }


            if (Vector3.Distance(boss.rightHand.transform.position, boss.rightHandRestingPosition.position) < 0.05f)
            {
                boss.rightHandHasSwiped = false; 
                controller.readyToGoNextState = true;
            }
        }



    }

    public override void Init(StateController controller)
    {
        boss = controller.GetComponent<RobotBoss>();
        leftHandAnimator = controller.GetComponent<RobotBoss>().leftHandanimater;
        rightHandAnimator = controller.GetComponent<RobotBoss>().rightHandAnimator;

    }
}
  