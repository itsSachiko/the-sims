using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirstState : Sim
{
    GameObject target;
    float time;
    Vector3 targetPos;
    Vector3 agentPos;
    public override void EnterState(StateManager sim)
    {
        target = null;
        Debug.Log("im thirsty");
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
                Debug.Log("i have arrived");
                if (time < sim.drinkingDuration)
                {
                    time += Time.deltaTime;
                }
                else
                {
                    sim.currentThirst = 0;
                    Debug.Log("i have drank");

                    time = 0;
                    target = null;
                    OnExit(sim);

                    sim.ChangeState(new WorkState());

                }
            }
        }

        else
        {
            target = sim.water;
            
            targetPos = target.transform.position;
            targetPos.y = 0;
            sim.agent.SetDestination(targetPos);
        }

    }

    public override void OnExit(StateManager sim)
    {

    }
}
