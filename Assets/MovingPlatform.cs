using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 originalScale = collision.transform.localScale;
            collision.transform.SetParent(transform, true);
            Debug.Log("Set parent");
            //Set Parent;
            collision.transform.localScale = originalScale;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
