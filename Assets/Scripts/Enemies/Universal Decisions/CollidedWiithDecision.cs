using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/CollisionDecisioin")]
public class CollidedWiithDecision : Decision
{
    [SerializeField] string colldierToCheckName;
    public override bool Decide(StateController controller)
    {
        if (controller.hitColldier != null && controller.hitColldier.name == colldierToCheckName)
        {
            return true;  
        }
        else{
            return false;
        }
    }
}
