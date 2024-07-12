using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkState : Sim
{
    GameObject target;
    
    float time;
    Vector3 targetPos;
    Vector3 agentPos;
    public override void EnterState(StateManager sim)
    {
        //    target = sim.GetTarget(sim.trees);
        //    sim.agent.SetDestination(target.transform.position);
        target = null;
        //sim.hasCutTree = false;
        time = 0;
        Debug.Log("im working");
    }

    public override void UpdateState(StateManager sim)
    {


        if (target != null)
        {
            agentPos = sim.transform.position;
            agentPos.y = 0;
            Debug.LogWarning(Vector3.Distance(agentPos, targetPos));

            sim.currentHunger += sim.hungerPerSec * Time.deltaTime;
            sim.currentThirst += sim.thirstPerSec * Time.deltaTime;
            if (sim.currentHunger >= sim.maxHunger)
            {
                OnExit(sim);
                sim.ChangeState(new HungerState());
            }

            else if (sim.currentThirst >= sim.maxThirst)
            {
                OnExit(sim);
                sim.ChangeState(new ThirstState());
            }

            if (Vector3.Distance(agentPos, targetPos) <= sim.agent.stoppingDistance)
            {
                Debug.Log("i have arrived");
                if (time < sim.cutTreeDuration)
                {
                    time += Time.deltaTime;
                }
                else
                {
                    Debug.Log("i have cut the tree");
                    sim.woodLog.SetActive(false);
                    if (!sim.hasCutTree)
                    {
                        target.SetActive(false);
                        sim.woodLog.SetActive(true);
                    }
                    time = 0;
                    target = null;
                    sim.hasCutTree = !sim.hasCutTree;
                }
            }
        }
        else
        {
            if (sim.hasCutTree)
            {
                target = sim.WareHouse;
            }
            else
            {
                target = sim.GetTarget(sim.trees);
            }
            if (target == null)
            {
                return;
            }
            targetPos = target.transform.position;
            targetPos.y = 0;
            sim.agent.SetDestination(targetPos);
        }
    }

    public override void OnExit(StateManager sim)
    {

    }
}
