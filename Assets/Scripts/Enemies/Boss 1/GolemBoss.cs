using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class GolemBoss : MonoBehaviour
{

    private StateMachine m_Machine;
    private GameObject bullet;
    public GameObject player;
    private Animator animator;


    public bool secondPhase;
    public bool shootingEnabled;
    public bool laserEnabled;

    [SerializeField] private float shootCD;
    private float shootTimer;
    private float laserCD;
    private float laserTimer;

    IState idleState, shootState;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Animator animator = GetComponent<Animator>();



        m_Machine = new StateMachine();

        idleState = new IdleState(animator);
        shootState = new ShootState(bullet,m_Machine,gameObject, idleState, animator);
        m_Machine.AddState(idleState);
        m_Machine.AddState(shootState);

        m_Machine.Initialize(idleState);


        shootTimer = 0;
        shootCD = 0;
        laserCD = 0;
        laserTimer = 0;

        gameObject.transform.position = new Vector3(player.transform.position.x - 10, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        m_Machine.Update();

        gameObject.transform.position = new Vector3(player.transform.position.x - 10, transform.position.y, transform.position.z);

        if (shootingEnabled)
        {
            if(m_Machine.CurrentState == idleState)
            {
                shootTimer += Time.deltaTime;
            }

            if (shootTimer >= shootCD) {
                shootTimer = 0;
                m_Machine.ChangeState(shootState);
            
            
            }
        }


        if (laserEnabled) { 
        
        
        
        }
    }
}
