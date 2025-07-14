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








    private void Awake()
    {
        leftHandanimater = leftHand.GetComponent<Animator>();
        leftHandRigidbody = leftHand.GetComponent<Rigidbody2D>();

        rightHandAnimator = rightHand.GetComponent<Animator>();
        rightHandRigidbody = rightHand.GetComponent<Rigidbody2D>();
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
        
    }
}
