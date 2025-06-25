using System;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/TimerDecision")]
public class TimerDecision : Decision
{

    [SerializeField] private float timeToSwitch = 3.0f;
    public override bool Decide(StateController controller)
    {
        return TimerFinished(controller);
    }

    private bool TimerFinished(StateController controller)
    {
        return controller.CheckIfCountDownElpased(timeToSwitch);
    }
}
