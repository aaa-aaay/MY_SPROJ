using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Robot/RandomMove")]
public class RobotMoveAction : Action
{


    private GameObject obj;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float pauseDuration = 1f;
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;

    RobotBoss boss;

    private Vector3 targetPos;
    private bool isMoving = false;

    public override void Init(StateController controller)
    {
        isMoving = true;
        boss = controller.gameObject.GetComponent<RobotBoss>();
        obj = controller.gameObject;
        boss.timer = 0;
        SetNewTargetPosition();
    }

    public override void Act(StateController controller)
    {
        boss = controller.gameObject.GetComponent<RobotBoss>();
        obj = controller.gameObject;


        if (!isMoving)
        {
            boss.timer -= Time.deltaTime;
            if (boss.timer <= 0f)
            {
                SetNewTargetPosition();
                isMoving = true;
            }
            return;
        }

        // Move toward target
        obj.transform.position = Vector3.MoveTowards(
            obj.transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        // Reached target
        if (Vector3.Distance(obj.transform.position, targetPos) < 0.01f)
        {
            isMoving = false;
            boss.timer = pauseDuration;
        }
    }

    private void SetNewTargetPosition()
    {
        float randomX = Random.Range(minX, maxX);
        targetPos = new Vector3(randomX, obj.transform.position.y, obj.transform.position.z);
    }
}
