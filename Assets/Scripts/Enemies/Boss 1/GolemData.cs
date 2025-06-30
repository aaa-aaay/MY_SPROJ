using UnityEngine;

public class GolemData : MonoBehaviour
{
    [HideInInspector] public float dodgeTimer = 0.0f;
    [HideInInspector] public float dodgeTargetY;
    [SerializeField] public GolemLaser laser;
    public bool LaserFinished;
    public float laserTimer= 3; 
    public float laserCD = 3;



    //chaseing
    public float ChaseDistance = 15;
    public float dodgespeed = 1;
    public float dodgeAmplitude = 1.5f;
    public float dodgeCooldown = 3;
    public float MinY = -7;
    public float MaxY = 0.5f;




    [SerializeField] private float phase2MinY = -23;
    [SerializeField] private float phase2MaxY = 28f;


    private bool phase2 = false;

    private void Start()
    {
        phase2 = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Boss Phase 2")
        {
            phase2 = true;
            MinY = phase2MinY;
            MaxY = phase2MaxY;
            laserCD = 3;

        }
    }
}
