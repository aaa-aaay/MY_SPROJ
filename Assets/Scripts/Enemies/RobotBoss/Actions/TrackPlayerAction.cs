using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Robot/HandsTrackPlayer")]
public class TrackPlayerAction : Action
{
    private RobotBoss boss;
    public override void Act(StateController controller)
    {


        Transform p1 = PlayerManager.Instance.GetPlayer1().transform;
        Transform p2 = PlayerManager.Instance.GetPlayer2().transform;

        float p1xValue = p1.position.x;
        float p2xValue = p2.position.x;

        // Determine left and right hand targets based on player x values
        Transform leftTarget = p1xValue > p2xValue ? p1 : p2;
        Transform rightTarget = p1xValue > p2xValue ? p2 : p1;

        // Lerp left hand
        Vector3 leftHandTargetPos = new Vector3(leftTarget.position.x, boss.leftHand.transform.position.y, boss.leftHand.transform.position.z);


        // Lerp right hand
        Vector3 rightHandTargetPos = new Vector3(rightTarget.position.x, boss.rightHand.transform.position.y, boss.rightHand.transform.position.z);




        if (!boss.leftHandHasSwiped && !boss.rightHandHasSwiped)
        {


            if (Vector3.Distance(boss.leftHand.transform.position, leftHandTargetPos) > 0.05f && Vector3.Distance(boss.rightHand.transform.position, rightHandTargetPos) > 0.05f)
            {

                boss.leftHand.transform.position = Vector3.Lerp(
                boss.leftHand.transform.position,
                leftHandTargetPos,
                Time.deltaTime * boss.speedToGoAttackPosition);



                boss.rightHand.transform.position = Vector3.Lerp(
                boss.rightHand.transform.position,
                rightHandTargetPos,
                Time.deltaTime * boss.speedToGoAttackPosition);


            }
            else
            {
                if (!controller.CheckIfCountDownElpased(0.2f)) return;
                boss.leftHandRigidbody.AddForce(Vector2.down * boss.swipeAttackSpeed, ForceMode2D.Impulse);
                boss.rightHandRigidbody.AddForce(Vector2.down * boss.swipeAttackSpeed, ForceMode2D.Impulse);

                boss.leftHandHasSwiped = true;
                boss.rightHandHasSwiped = true;
            }

        }
        else
        {

            if (!controller.CheckIfCountDownElpasedSecond(1)) return;

            if (boss.leftHandRigidbody.linearVelocity.magnitude < 0.1f && boss.rightHandRigidbody.linearVelocity.magnitude < 0.1f)
            {

                boss.leftHandanimater.Play("TurnToPalm");
                boss.rightHandAnimator.Play("TurnToPalm");

                boss.leftHand.transform.position = Vector3.Lerp(
                boss.leftHand.transform.position,
                boss.leftHandRestingPosition.position,
                Time.deltaTime * boss.speedToGoAttackPosition);


                boss.rightHand.transform.position = Vector3.Lerp(
                boss.rightHand.transform.position,
                boss.rightHandRestingPosition.position,
                Time.deltaTime * boss.speedToGoAttackPosition);

                if (Vector3.Distance(boss.leftHand.transform.position, boss.leftHandRestingPosition.position) < 0.05f)
                {
                    boss.leftHand.transform.position = boss.leftHandRestingPosition.position;
                }

                if (Vector3.Distance(boss.rightHand.transform.position, boss.rightHandRestingPosition.position) < 0.05f)
                {
                    boss.rightHand.transform.position = boss.rightHandRestingPosition.position;
                }






            }


            if (Vector3.Distance(boss.leftHand.transform.position, boss.leftHandRestingPosition.position) < 0.05f && Vector3.Distance(boss.rightHand.transform.position, boss.rightHandRestingPosition.position) < 0.05f)
            {
                boss.leftHandHasSwiped = false;
                boss.rightHandHasSwiped = false;
                controller.readyToGoNextState = true;
            }





        }


        //find the x position of both the players
        //right x is set to the player with a lower x value
        //left hand is set to the other

    }

    public override void Init(StateController controller)
    {
        boss = controller.GetComponent<RobotBoss>();
        boss.rightHandAnimator.Play("TurnToFist");
        boss.leftHandanimater.Play("TurnToFist");
    }
}
