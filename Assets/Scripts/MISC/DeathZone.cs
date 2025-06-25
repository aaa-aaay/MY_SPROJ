using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        IDeath dying = collision.gameObject.GetComponent<IDeath>();
        if (dying != null) {

            if (dying.IsDead) return;
            dying.StartDying();


        }
    }
}
