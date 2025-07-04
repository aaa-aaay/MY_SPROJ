using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private bool laserActive;
    private Collider2D laserCollider;
    private Animator animator;
    [SerializeField] private PuzzleButton deactivatebutton;

    private void Awake()
    {
        laserActive = true;
        laserCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        laserCollider.enabled = true;
    }

    public void EnableLaser()
    {
        laserActive = true;
        animator.SetTrigger("ActivateLaser");
        laserCollider.enabled = true;
    }

    public void DisableLaser()
    {
        
        laserActive = false;
        animator.SetTrigger("DeactivateLaser");
        laserCollider.enabled = false;

    }

    private void Update()
    {
        if(deactivatebutton == null) return;

        if (deactivatebutton.buttonPressed)
        {
            deactivatebutton.buttonPressed = false;
            if(laserActive) DisableLaser();
            else EnableLaser();
        }


    }

}
