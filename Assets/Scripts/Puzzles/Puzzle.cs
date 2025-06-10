using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    [SerializeField] protected PuzzleButton button1;
    [SerializeField] protected PuzzleButton button2;

    protected abstract void PuzzleOver();
}


