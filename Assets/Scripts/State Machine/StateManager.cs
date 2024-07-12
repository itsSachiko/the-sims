using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    public Sim currentState;

    [HideInInspector] public NavMeshAgent agent;
    public GameObject woodLog;

    public ObjectPooler trees;
    public ObjectPooler food;
    public GameObject house;

    public float cutTreeDuration;

    public GameObject WareHouse;

    public GameObject water;

    [HideInInspector] public float currentThirst;
    [HideInInspector] public float currentHunger;

    public float maxThirst;
    public float maxHunger;

    public float hungerPerSec;
    public float thirstPerSec;

    public float eatingDuration;
    public float drinkingDuration;

    [HideInInspector] public bool hasCutTree;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnEnable()
    {
        Clock.onTimeShift += OnTimeShift;
    }

    private void OnDisable()
    {
        Clock.onTimeShift -= OnTimeShift;
    }

    private void OnTimeShift(bool isDay)
    {
        currentState.OnExit(this);
        if (isDay)
        {
            ChangeState(new WorkState());
        }
        else
        {
            ChangeState(new SleepState());
        }
    }

    void Start()
    {
        ChangeState(new WorkState());
    }

    void Update()
    {
        currentState.UpdateState(this);
        //Debug.Log("hunger "+ currentHunger);
        //Debug.Log("thirst "+ currentThirst);
    }

    public void ChangeState(Sim state)
    {
        currentState = state;
        state.EnterState(this);
    }
    public GameObject GetTarget(ObjectPooler pooler)
    {
        foreach (var item in pooler.objects)
        {
            if (item.activeInHierarchy)
            {
                return item;
            }
        }
        return null;
    }
}
