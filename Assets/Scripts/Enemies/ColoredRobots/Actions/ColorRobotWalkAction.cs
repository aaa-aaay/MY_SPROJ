using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/RobotDash")]
public class ColorRobotWalkAction : Action
{
    private GameObject player1;
    private GameObject player2;
    private GameObject targetPlayer;
    private bool hasDashed = false;

    private float objSize;


    public override void Act(StateController controller)
    {
        Rigidbody2D rb = controller.GetComponent<Rigidbody2D>();
        ColoredRobots robot = controller.GetComponent<ColoredRobots>();
        if (targetPlayer == null) return;

        if (!hasDashed)
        {
            controller.animator.Play("walk");



            Vector3 scale = controller.transform.localScale;

            if (targetPlayer.transform.position.x < controller.transform.position.x)
                scale.x = -Mathf.Abs(scale.x); // Face left
            else
                scale.x = Mathf.Abs(scale.x); // Face right

            controller.transform.localScale = scale;

            // Dash toward target
            float direction = targetPlayer.transform.position.x < controller.transform.position.x ? -1f : 1f;
            rb.linearVelocity = new Vector2(direction * robot.dashForce, rb.linearVelocity.y);

            hasDashed = true;
        }

        // Wait until velocity is close to 0 before transitioning
        if (Mathf.Abs(rb.linearVelocity.x) < 0.1f)
        {
            controller.readyToGoNextState = true;
        }
    }

    public override void Init(StateController controller)
    {
        player1 = PlayerManager.Instance.player1.gameObject;
        player2 = PlayerManager.Instance.player2.gameObject;
        hasDashed = false;
        Vector3 originalScale = controller.transform.localScale;


        // Find the closest player
        float distToP1 = Vector2.Distance(controller.transform.position, player1.transform.position);
        float distToP2 = Vector2.Distance(controller.transform.position, player2.transform.position);
        targetPlayer = distToP1 < distToP2 ? player1 : player2;

        Debug.Log("This is called");
    }
}

