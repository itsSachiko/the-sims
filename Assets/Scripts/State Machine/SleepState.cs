using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SleepState : Sim
{
    GameObject target;
    float time;
    Vector3 targetPos;
    Vector3 agentPos;
    public override void EnterState(StateManager sim)
    {
        target = null;
        Debug.Log("im sleeping");
    }

    public override void UpdateState(StateManager sim)
    {
        if (target != null)
        {
            agentPos = sim.transform.position;
            agentPos.y = 0;
            Debug.LogWarning(Vector3.Distance(agentPos, targetPos));

            if (Vector3.Distance(agentPos, targetPos) <= sim.agent.stoppingDistance)
            {
                return;
            }
        }

        else
        {
            target = sim.house;

            targetPos = target.transform.position;
            targetPos.y = 0;
            sim.agent.SetDestination(targetPos);
        }

    }

    public override void OnExit(StateManager sim)
    {

    }
}
