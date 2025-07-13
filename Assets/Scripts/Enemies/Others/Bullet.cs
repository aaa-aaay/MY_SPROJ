using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Bullet : MonoBehaviour
{
    protected Transform bulletTarget;
    public LayerMask interactableLayers;
    [SerializeField] protected int damage;
    protected Explosion explosion;


    public abstract void Shoot(Vector2 direction = default, float power = 0);
    public virtual void SetInputAction(InputAction action) { }

    public void SetTarget(Transform target)
    {

        bulletTarget = target;  
    
    }

    public virtual void DestroyBullet()
    {

        if (explosion != null) {
            explosion.TriggerExplosoin(gameObject);
        } 

        else Destroy(gameObject);
        //play sound
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        explosion = GetComponentInChildren<Explosion>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
