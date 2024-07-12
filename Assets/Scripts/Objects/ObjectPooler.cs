using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public List<GameObject> objects = new();
    public GameObject prefabs;
    public int numberToSpawn;
    public float spawnRange;
    public float activatedADay;
    private void Awake()
    {
        Vector3 randomSpawnPos;
        for (int i = 0; i < numberToSpawn; i++)
        {
            randomSpawnPos = new Vector3(Random.Range(0f, spawnRange / 2f), prefabs.transform.position.y, Random.Range(0f, spawnRange / 2f));
            var spawnedObject = Instantiate(prefabs, transform);
            spawnedObject.transform.localPosition = randomSpawnPos;
            objects.Add(spawnedObject);
            spawnedObject.SetActive(false);
        }

        for (int i = 0; i < activatedADay; i++)
        {
            var random = Random.Range(0, objects.Count);
            if (objects[random].activeInHierarchy)
            {
                i--;
            }
            objects[random].SetActive(true);
        }

    }

    private void OnEnable()
    {
        Clock.onTimeShift += RespawnObjects;
    }

    private void OnDisable()
    {
        Clock.onTimeShift -= RespawnObjects;
    }
    void RespawnObjects(bool isDay)
    {
        if (isDay)
        {
            return;
        }
        int tries = 0;
        for (int i = 0; i < activatedADay; i++)
        {

            Debug.Log(i);

            var random = Random.Range(0, objects.Count);
            if (objects[random].activeInHierarchy)
            {

                if (i - 1 <= 0)
                {
                    i = 0;
                }
                else
                {
                    i--;
                }
                //i = i-1 <= 0 ? 0 : i--;
                tries++;
                if (tries >= numberToSpawn * 2)
                {
                    return;
                }
                Debug.Log("object already active " + i, objects[random]);

            }

            else
            {
                objects[random].SetActive(true);

            }
        }
    }
}
