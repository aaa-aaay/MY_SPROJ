using System.Diagnostics.Contracts;
using UnityEngine;

public abstract class Decision : ScriptableObject
{
    public abstract bool Decide(StateController controller);
}
