using UnityEngine;

public class PlayerJoinScript : MonoBehaviour
{
    public Transform spawnPoint1, spawnPoint2;

    public GameObject p1, p2;

    private void Awake()
    {
        Instantiate(p1, spawnPoint1.position,Quaternion.identity);
        Instantiate(p2, spawnPoint2.position,Quaternion.identity);
    }
     
}
