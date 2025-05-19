using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;

    private AnimationState currentState;

    public enum AnimationState
    {
        Idle,
        Run,
        Jump,
        Fall,
        Attack,
        Dash,
        Climb,
        Death
    }

    private Dictionary<AnimationState, string> animationStates = new Dictionary<AnimationState, string>()
    {
        { AnimationState.Idle, "Player_Idle" },
        { AnimationState.Run, "Player_Run" },
        { AnimationState.Jump, "Player_Jump" },
        { AnimationState.Fall, "Player_Fall" },
        { AnimationState.Attack, "Player_Attack" },
        { AnimationState.Dash, "Player_Dash" },
        { AnimationState.Climb, "Player_Climb" },
        { AnimationState.Death, "Player_Death" }
    };

    public void ChangeAnimationState(AnimationState newState)
    {
        if (currentState == newState) return;

        animator.Play(animationStates[newState]);
        currentState = newState;
    }
    public void ToggleAnimaiton()
    {
        animator.enabled = !animator.enabled;
    }
}
