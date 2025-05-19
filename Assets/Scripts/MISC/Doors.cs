using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] DoorButton button1;
    [SerializeField] DoorButton button2;
    private Animator animator;
    private bool animationPlayed = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(button1.buttonPressed && button2.buttonPressed && !animationPlayed)
        {
            animator.SetTrigger("OpenDoor");
            animationPlayed = true;
        }
    }

}
