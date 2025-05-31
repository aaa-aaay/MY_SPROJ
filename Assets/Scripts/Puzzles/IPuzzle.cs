using UnityEngine;

public abstract class IPuzzle : MonoBehaviour
{

    [SerializeField] protected PuzzleButton button1;
    [SerializeField] protected PuzzleButton button2;

    protected abstract void PuzzleOver();
}
