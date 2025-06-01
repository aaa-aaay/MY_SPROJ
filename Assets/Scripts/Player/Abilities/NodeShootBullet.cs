using UnityEngine;
using UnityEngine.InputSystem;

public class NodeShootBullet : MonoBehaviour
{
    [SerializeField] private GameObject GrappleNode;
    [SerializeField] LayerMask interactableWallLayer;
    [SerializeField] LayerMask wallLayer;
    private InputAction placeDownAction;
    private bool canPlaceDown;
    private void PlaceNode()
    {
        Instantiate(GrappleNode,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }

    public void SetInput(InputAction action)
    {
        placeDownAction = action;
    }

    private void Update()
    {
        if (canPlaceDown) {


            if (placeDownAction.WasPressedThisFrame())
            {
                PlaceNode();
            }
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & wallLayer.value) != 0)
        {
            Destroy(gameObject);
        }


        if (((1 << collision.gameObject.layer) & interactableWallLayer.value) != 0)
        {
            canPlaceDown = true;
            //can place down node
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & interactableWallLayer.value) != 0)
        {
            canPlaceDown = false;
            //can place down node
        }
    }


}
