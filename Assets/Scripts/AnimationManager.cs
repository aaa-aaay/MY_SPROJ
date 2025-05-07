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
        Roll,
        Climb
    }

    private Dictionary<AnimationState, string> animationStates = new Dictionary<AnimationState, string>()
    {
        { AnimationState.Idle, "Player_Idle" },
        { AnimationState.Run, "Player_Run" },
        { AnimationState.Jump, "HeroKnight_Jump" },
        { AnimationState.Fall, "HeroKnight_Fall 0" },
        { AnimationState.Attack, "HeroKnight_Attack1" },
        { AnimationState.Roll, "HeroKnight_Roll" },
        { AnimationState.Climb, "HeroKnight_Climb" }
    };

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(AnimationState newState)
    {
        if (currentState == newState) return;

        animator.Play(animationStates[newState]);
        currentState = newState;
    }
}
