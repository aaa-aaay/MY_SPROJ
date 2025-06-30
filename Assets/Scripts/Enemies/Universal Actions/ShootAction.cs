using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Actions/Shoot")]
public class ShootAction : Action
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootLocation;
    public override void Act(StateController controller)
    {
        Animator animator = controller.animator;

        // Only trigger once
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("shoot"))
        {
            animator.Play("shoot");
        }

        CheckAnimationFinished(controller);
    }

    public override void Init(StateController controller)
    {

    }

    private void CheckAnimationFinished(StateController controller)
    {
        Animator animator = controller.animator;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("shoot") && stateInfo.normalizedTime >= 1f)
        {
            Transform shootLocation = controller.transform.Find("Shoot location");
            if (shootLocation != null) Debug.Log("found shoot location");
            GameObject bulletOBJ = Instantiate(bulletPrefab, shootLocation.position, Quaternion.identity);
            Bullet bullet = bulletOBJ.GetComponent<Bullet>();
            if(bullet != null)
            {
                bullet.SetTarget(controller.player.transform);
                bullet.Shoot();
            }



            controller.readyToGoNextState = true;
        }
    }
}
