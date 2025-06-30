using JetBrains.Annotations;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected Transform bulletTarget;
    public LayerMask interactableLayers;
    [SerializeField] private float damage;


    public abstract void Shoot(Vector2 direction = default, float power = 0);

    public void SetTarget(Transform target)
    {

        bulletTarget = target;  
    
    }

    public virtual void DestroyBullet()
    {
        Destroy(gameObject);
        //play animation
        //destory bullet
        //play sound
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
