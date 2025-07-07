using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private int forPlayerNo;
    private float startPos, length;
    public GameObject cam;
    public float parallaxEffect; //speed at which the background should move
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = CameraManager.Instance.GetPlayerCamera(forPlayerNo).gameObject;
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (movement > startPos + length)
        {

            startPos += length;




        }
        else if (movement < startPos - length)
        {
            startPos += length;



        }
    }
}

