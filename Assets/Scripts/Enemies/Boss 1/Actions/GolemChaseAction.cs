using NUnit.Framework;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Actions/Golem/GolemChase")]
public class GolemChaseAction : Action
{
    private float chaseDistance = 9.0f;
    private float dodgeSpeed = 1.0f;
    private float dodgeAmplitude = 1.0f;
    private float dodgeCooldown = 2f;
    private float minY = 2.0f;
    private float maxY = 5.0f;

    private float dodgeTimer = 0f;
    public override void Act(StateController controller)
    {
        var data = controller.GetComponent<GolemData>();



        chaseDistance = data.ChaseDistance;
        dodgeSpeed = data.dodgespeed;
        dodgeAmplitude = data.dodgeAmplitude;
        dodgeCooldown = data.dodgeCooldown;
        minY = data.MinY;
        maxY = data.MaxY;


        Dodge(controller, data);
        FollowPlayer(controller, data);
    }

    public override void Init(StateController controller)
    {

    }

    private void Dodge(StateController controller, GolemData data)
    {
        data.dodgeTimer += Time.deltaTime * dodgeSpeed;


        if (data.dodgeTimer >= dodgeCooldown)
        {
            data.dodgeTargetY = Random.Range(minY, maxY);
            data.dodgeTimer = 0f;
        }

    }

    private void FollowPlayer(StateController controller, GolemData data)
    {

        float targetX = controller.player.transform.position.x - chaseDistance;
        float targetY = Mathf.Lerp(controller.transform.position.y, data.dodgeTargetY, Time.deltaTime * dodgeAmplitude);

        Vector3 targetPos = new Vector3(targetX, targetY, controller.transform.position.z);
        controller.transform.position = new Vector3(targetX, targetY, controller.transform.position.z);
    }

}
