using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject clockHand;
    public Image hungerBar;
    public Image thirstBar;
    StateManager player;
    float inGameCompleteDay = 40f;
    Clock clock;
    float day;
    private void Awake()
    {
        player = FindObjectOfType<StateManager>();
        
    }
    private void Start()
    {
       
        
    }

    private void Update()
    {
        hungerBar.fillAmount = player.currentHunger / player.maxHunger;
        thirstBar.fillAmount = player.currentThirst / player.maxThirst;

        day += Time.deltaTime / inGameCompleteDay;
        float dayNormalized = day % 1f;
        clockHand.transform.eulerAngles = new Vector3(0, 0, -dayNormalized * 360f);
    }
}
