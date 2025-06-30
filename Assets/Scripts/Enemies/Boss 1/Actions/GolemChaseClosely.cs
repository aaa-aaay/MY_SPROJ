using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Golem/GolemCloseCahse")]
public class GolemChaseClosely : Action
{
    [SerializeField] private float chaseDistance = 9.0f;
    [SerializeField] private float offsetY = -2.0f;
    [SerializeField] float moveSpeed = 10;

    public override void Act(StateController controller)
    {

        float targetX = controller.player.transform.position.x - chaseDistance;
        float targetY = Mathf.Lerp(controller.transform.position.y, controller.player.transform.position.y + offsetY, Time.deltaTime * moveSpeed);

        controller.transform.position = new Vector3(targetX, targetY, controller.transform.position.z);

    }

    public override void Init(StateController controller)
    {
    }



}
