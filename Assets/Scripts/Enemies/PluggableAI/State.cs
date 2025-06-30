using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transition[] transitions;
    public Color sceneGizmoColor = Color.gray;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransition(controller);
    }

    private void DoActions(StateController controller)
    {
        foreach (Action action in actions)
        {
            action.Act(controller);
        }
    }

    private void InitActions(StateController controller)
    {
        foreach(Action action in actions)
        {
            action.Init(controller);
        }
    }

    private void CheckTransition(StateController controller)
    {
        foreach (Transition transition in transitions)
        {
            bool decisionSucceeded = transition.decision.Decide(controller);

            if (decisionSucceeded) { 
                controller.TransitionToState (transition.trueState);
            
            }
            else
            {
                controller.TransitionToState (transition.falseState);
            }
        }
    }
}

