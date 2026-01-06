using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public BoxCollider2D spawnCheck;
    public List<Vector3> possibleSpawnPoints;
    private static FoodSpawner instance;

    private void Start()
    {
        instance = this;
        possibleSpawnPoints = new List<Vector3>();

        for (float y = 4; y >= -4; y -= .5f)
        {
            for (float x = -4; x <= 4; x += .5f)
            {
                Vector3 pos = new Vector3(x, y, 0.0f);
                possibleSpawnPoints.Add(pos);

            }
        }

        SpawnFood();

    }

    // work in progess, issue: can randomly spawn on player body
    public void SpawnFood()
    {
        bool safe = false;
        Vector3 firstFoodSpawn = possibleSpawnPoints[Random.Range(0, possibleSpawnPoints.Count)];
        List<GameObject> bodyParts = Snake.GetSnake().GetBodyParts();

        while (!safe)
        {
            for (int i = 0; i < bodyParts.Count; i++)
            {
                if (bodyParts[i].transform.position == firstFoodSpawn)
                {
                    firstFoodSpawn = possibleSpawnPoints[Random.Range(0, possibleSpawnPoints.Count)];
                    Debug.Log(firstFoodSpawn + " was NOT safe to spawn food. CHECKING AGAIN.");
                    return;
                }
                else
                {
                    Debug.Log(firstFoodSpawn + " was safe to spawn food.");
                    safe = true;
                    break;
                }
            }
        }

        if (firstFoodSpawn == Vector3.zero)
        {
            firstFoodSpawn = Vector3.right * 3f;
        }

        Instantiate(foodPrefab, firstFoodSpawn, Quaternion.identity);
    }



    public static FoodSpawner GetInstance()
    {
        return instance;
    }
}
