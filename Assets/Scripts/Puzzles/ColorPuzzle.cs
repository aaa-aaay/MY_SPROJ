using UnityEngine;

public abstract class ColorPuzzle : MonoBehaviour
{

    [SerializeField] private bool redColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LaserBullet bullet = collision.GetComponent<LaserBullet>();

        if(bullet.redBullet && redColor)
        {
           Destroy(bullet.gameObject);
            Interact();
        }
        if(!bullet.redBullet && !redColor)
        {
           Destroy(bullet.gameObject);
            Interact();
        }
    }


    protected abstract void Interact();

}
