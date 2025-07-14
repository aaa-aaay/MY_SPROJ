using UnityEngine;

public class SwitchSceneOnCollide : MonoBehaviour
{

    [SerializeField] int nextScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<ShipControls>() != null || collision.gameObject.GetComponent<PlayerController>())
        {
            MySceneManager.Instance.GoNextScene(nextScene);
        }
    }
}
