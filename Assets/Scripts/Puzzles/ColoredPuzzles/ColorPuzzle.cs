using UnityEngine;

public abstract class ColorPuzzle : MonoBehaviour
{

    [SerializeField] protected bool redColor;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        ColoredAbilities bullet = collision.GetComponent<ColoredAbilities>();
        if(bullet == null ) return;
        if(collision.GetComponent<ColorGrenade>()) return;

        if(bullet.IsRedColor && redColor)   
        {
           Destroy(collision.gameObject);
            Interact();
        }
        if(!bullet.IsRedColor && !redColor)
        {
           Destroy(collision.gameObject);
            Interact();
        }
    }


    public abstract void Interact();

}
