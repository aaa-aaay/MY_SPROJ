using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Decisions/Golem/CollisionDecisioin")]
public class LaserShootDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        GolemData golem = controller.gameObject.GetComponent<GolemData>();
        if (golem.laserTimer >= golem.laserCD)
        {
            golem.laserTimer = 0;
            return true;
        }

        golem.laserTimer += Time.deltaTime;
        return false;
    }


}
    