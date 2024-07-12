using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [HideInInspector] public float time;
    public float dayDuration;
    public float nightDuration;

    bool isDay = true;

    public static event Action<bool> onTimeShift;

    Light worldLight;

    private void Awake()
    {
        worldLight = FindObjectOfType<Light>();
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (isDay)
        {

            if (time >= dayDuration)
            {
                worldLight.enabled = false;
                isDay = false;
                Debug.Log("its night");
                onTimeShift?.Invoke(isDay);
            }
        }

        else
        {

            if (time >= dayDuration + nightDuration)
            {
                worldLight.enabled = true;
                isDay = true;
                time = 0;
                Debug.Log("its morning");
                onTimeShift?.Invoke(isDay);
            }
        }
    }
}

