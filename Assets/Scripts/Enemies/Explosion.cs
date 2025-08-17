using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    Animator animator;
    private bool isExploding = false;
    private GameObject objToDestory;
    private SpriteRenderer sprite;
    private void Start()
    {
        animator = GetComponent<Animator>();
        //TriggerExplosoin();


    }


    public void TriggerExplosoin(GameObject obj)
    {
        animator.SetTrigger("explode");
        objToDestory = obj;
        obj.GetComponent<SpriteRenderer>().enabled = false;
        isExploding = true;
        Debug.Log("triggered exoplsoion");
    }

    private void Update()
    {
        if (isExploding)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("explosion_Clip") && stateInfo.normalizedTime >= 1f)
            {

                Debug.Log("destoryed bullet");
                // Animation finished
                isExploding = false;
                // Do something, e.g. Destroy(gameObject);
                Destroy(objToDestory);
            }
        }
    }

    public void TriggerExplosionSFX()
    {
        AudioManager.instance.PlaySFX("SFXExplosion");
    }





}
