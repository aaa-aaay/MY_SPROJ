using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class RobotBoss : MonoBehaviour
{

    [SerializeField] public GameObject leftHand;
    [HideInInspector] public Animator leftHandanimater;
    [HideInInspector] public Rigidbody2D leftHandRigidbody;

    [SerializeField] public GameObject rightHand;
    [HideInInspector]public Animator rightHandAnimator;
    [HideInInspector] public Rigidbody2D rightHandRigidbody;



    [SerializeField] public Transform rightHandRestingPosition;
    [SerializeField] public Transform rightHandSwipePosition;


    [Header("Swipe Settings")]
    [SerializeField] public Transform leftHandSwipePosition;
    [SerializeField] public Transform leftHandRestingPosition;
    [SerializeField] public float speedToGoAttackPosition = 5.0f;
    [SerializeField] public float swipeAttackSpeed = 5.0f;
    [HideInInspector] public bool rightHandHasSwiped;
    [HideInInspector] public bool leftHandHasSwiped;

    [Header("Apply Barrier")]
    [SerializeField] float barrierApplyTime;
    [SerializeField] GameObject blueBaarrierPlayfab;
    [SerializeField] GameObject redBarrierPlayfab;
    [SerializeField] Transform barrierLocation;



    [Header("ColorRobots")]
    [SerializeField] public GameObject blueRobot;
    [SerializeField] public GameObject redRobot;
    [HideInInspector] public bool rightRobotSummoned;
    [HideInInspector] public bool leftRobotSummoned;


    [HideInInspector] public float timer;

    private float barrierSummonTimer;
    private GameObject barrier;








    private void Awake()
    {
        leftHandanimater = leftHand.GetComponent<Animator>();
        leftHandRigidbody = leftHand.GetComponent<Rigidbody2D>();

        rightHandAnimator = rightHand.GetComponent<Animator>();
        rightHandRigidbody = rightHand.GetComponent<Rigidbody2D>();

        timer = 0;
        barrierSummonTimer = 0.0f;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rightHandHasSwiped = false;
        leftHandHasSwiped = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (barrier != null) return;
        barrierSummonTimer += Time.deltaTime;

        if (barrierSummonTimer >= barrierApplyTime)
        {
            GameObject barrierToSummon = redBarrierPlayfab;
            int randomValue = Random.Range(1, 3);

            if (randomValue == 1) {
                barrierToSummon = blueBaarrierPlayfab;
            }

            barrierSummonTimer = 0;
            barrier = Instantiate(barrierToSummon, barrierLocation.position, Quaternion.identity);
            barrier.transform.SetParent(barrierLocation, false);
            barrier.transform.localPosition = Vector3.zero;
        }
    }



}
