using UnityEngine;

public class GolemLaser : MonoBehaviour
{
    private Animator animator;
    private Collider2D dmgCollider;
    private SpriteRenderer spriteRenderer;
    private GolemData golem;
    private bool shooting;
    private void Start()
    {
        animator = GetComponent<Animator>();
        dmgCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        golem = GetComponentInParent<GolemData>();
        spriteRenderer.enabled = false;
        dmgCollider.enabled = false;
        golem.LaserFinished = false;
    }

    public void StartLaser()
    {
        golem.LaserFinished = false;
        dmgCollider.enabled = false;
        spriteRenderer.enabled = true;
        animator.Play("laser_Clip",0,0);
    }

    public void ActivateLaserCollision()
    {   

        if(golem.LaserFinished == false)
        dmgCollider.enabled = true;
    }
    public void DeactivateLaserCollision()
    {

        spriteRenderer.enabled = false;
        dmgCollider.enabled = false;
        golem.LaserFinished = true;
    }

    private void Update()
    {
        
    }
}
