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
        Vector3 firstFoodSpawn = possibleSpawnPoints[Random.Range(0, possibleSpawnPoints.Count)];
        if (firstFoodSpawn == Vector3.zero)
        {
            firstFoodSpawn = Vector3.right * 3f;
        }

        Instantiate(foodPrefab, firstFoodSpawn, Quaternion.identity);
    }

    //private bool BodyAtPosition(Vector3 pos)
    //{
    //    return Physics2D.BoxCast(pos, spawnCheck.bounds.size, 0f, Vector2.zero, 0f, );
    //}

    public static FoodSpawner GetInstance()
    {
        return instance;
    }
}
