using Unity.Cinemachine;
using UnityEngine;

public class prepareForSlide : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController p = collision.gameObject.GetComponent<PlayerController>();
            p.jumpSpeed = 2;


            var cam = p.transform.parent.GetComponentInChildren<CinemachinePositionComposer>();

            cam.Damping = Vector3.zero;
            cam.TargetOffset = new Vector3(10, -3,0);


            p.disableHorizontalMove = true;
        }   
    }

}
