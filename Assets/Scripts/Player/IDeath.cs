
using UnityEngine;

public interface IDeath
{

    public Transform RespawnPosition { get; set; }
    public bool IsDead { get; set; }
    public void StartDying();

}
