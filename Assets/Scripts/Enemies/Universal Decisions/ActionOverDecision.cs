using System.Runtime.CompilerServices;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Decisions/ActionOverDecision")]
public class ActionOverDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return controller.readyToGoNextState;
    }
}
