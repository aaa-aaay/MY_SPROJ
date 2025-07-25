using UnityEngine;
using UnityEngine.InputSystem;

public class NodeShootBullet : MonoBehaviour
{
    [SerializeField] private GameObject GrappleNode;
    [SerializeField] LayerMask interactableWallLayer;
    [SerializeField] LayerMask wallLayer;
    private InputAction placeDownAction;
    private NodeShootAbility shootAbility;
    private bool canPlaceDown;
    private bool destoryed = false;
    private GameObject canPlaceOn;
    private void PlaceNode()
    {
        AudioManager.instance.PlaySFX("PlaceNode");
        GameObject node = Instantiate(GrappleNode,transform.position,Quaternion.identity);
        node.transform.SetParent(canPlaceOn.transform);
        if (shootAbility != null) { 
            shootAbility.listOfNodes.Add(node);
        }

        Destroy(gameObject);
    }

    public void InitBullet(InputAction action, NodeShootAbility ability)
    {
        placeDownAction = action;
        shootAbility = ability;
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
            if (shootAbility != null && !destoryed) {
                destoryed = true;
                shootAbility.GetBulletBack();

            
            
            }
        }


        if (((1 << collision.gameObject.layer) & interactableWallLayer.value) != 0)
        {
            canPlaceDown = true;
            canPlaceOn = collision.gameObject;
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
