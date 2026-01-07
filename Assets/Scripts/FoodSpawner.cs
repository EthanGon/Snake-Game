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

        

    }

    // work in progess, issue: can randomly spawn on player body
    public void SpawnFood()
    {
        Vector3 firstFoodSpawn = possibleSpawnPoints[Random.Range(0, possibleSpawnPoints.Count)];
  
        while (!IsSafe(firstFoodSpawn))
        {
            firstFoodSpawn = possibleSpawnPoints[Random.Range(0, possibleSpawnPoints.Count)];
        }
        
        Instantiate(foodPrefab, firstFoodSpawn, Quaternion.identity);
    }

    public bool IsSafe(Vector3 pos)
    {
        List<GameObject> snakeBodies = Snake.GetSnake().GetBodyParts();

        if (pos == Snake.GetSnake().transform.position)
        {
            Debug.Log(pos + " not safe, snake head located here. Randomizing position again.");
            return false;
        }

        foreach (GameObject body in snakeBodies)
        {
            if (body.transform.position == pos)
            {
                Debug.Log(pos + " not safe, snake body located here. Randomizing position again.");
                return false;
            }
        }

        return true;
    }



    public static FoodSpawner GetInstance()
    {
        return instance;
    }
}
