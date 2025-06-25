using UnityEngine;

public class GolemChaseState : IState
{
    private float teleportRange;
    private GameObject player;
    private GolemBoss golem;

    private float shootCD;
    private float shootTimer;
    private float laserCD;
    private float laserTimer;

    public GolemChaseState(GameObject p, GolemBoss golem)
    {
        player = p;
        this.golem = golem;
    }

    public void OnEnter()
    {
        golem.gameObject.transform.position = new Vector3(player.transform.position.x - 10, golem.transform.position.y, golem.transform.position.z);

        shootTimer = 0;
        shootCD = 0;
        laserCD = 0;
        laserTimer = 0;

    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        golem.gameObject.transform.position = new Vector3(player.transform.position.x - 10, golem.transform.position.y, golem.transform.position.z);
    }
}
