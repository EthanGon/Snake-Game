using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Snake : MonoBehaviour
{
    public Vector3 dir;
    private float timer;
    private static Snake SnakeObject;
    [SerializeField] private float interval;
    public Vector3 lastPos;
    private List<GameObject> bodyParts;
    public GameObject body;
    public bool snakeIsDead;
    public List<Vector3> dirInputs;
    public bool dirSet = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SnakeObject = this;

        snakeIsDead = false;
        bodyParts = new List<GameObject>();
        dir = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!dirSet)
        {
            SetSnakeDirection();
        }
        

        if (timer < interval && dir != Vector3.zero && !snakeIsDead)
        {
            timer = timer + Time.deltaTime;
        } 
        else
        {
            Vector3 nextPos = new Vector3(transform.position.x + dir.x, transform.position.y + dir.y, 0f);
            if (((WillHitWall(nextPos)) || WillHitBody(nextPos)) && timer >= interval - .5f)
            {
                Debug.Log(dir);
                snakeIsDead=true;
                return;
            }

            lastPos = transform.position;

            for (int i = 0; i < bodyParts.Count; i++)
            {
                bodyParts[i].GetComponent<Body>().MoveBody();
            }

            transform.position = new Vector3(transform.position.x + dir.x, transform.position.y + dir.y, 0.0f);
            dirSet = false;
            timer = 0.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("food"))
        {
            GameManager.GetInstance().IncreaseFoodCount();
            AddBody();
            

            FoodSpawner.GetInstance().SpawnFood();
            Destroy(collision.gameObject);
        }
    }

    public void AddBody()
    {
        if (bodyParts.Count == 0)
        {
            GameObject newTail = Instantiate(body, lastPos, Quaternion.identity);
            bodyParts.Add(newTail);
            newTail.GetComponent<Body>().next = this.gameObject;
        }
        else
        {
            Vector3 spawnPos = bodyParts[bodyParts.Count - 1].GetComponent<Body>().lastPos;

            GameObject newTail = Instantiate(body, spawnPos, Quaternion.identity);
            bodyParts.Add(newTail);
            newTail.GetComponent<Body>().next = bodyParts[bodyParts.Count - 2];
        }
    }

    public void SetSnakeDirection()
    {
        
        if (!snakeIsDead)
        {
            if (Input.GetKeyDown(KeyCode.D) && dir.x != -.5f)
            {
                dir = Vector2.right * .5f;
                dirInputs.Add(dir);
                dirSet = true;

            }
            else if (Input.GetKeyDown(KeyCode.A) && dir.x != .5f)
            {
                dir = Vector2.left * .5f;
                dirInputs.Add(dir);
                dirSet = true;
            }
            else if (Input.GetKeyDown(KeyCode.W) && dir.y != -.5f)
            {
                dir = Vector2.up * .5f;
                dirInputs.Add(dir);
                dirSet = true;
            } 
            else if (Input.GetKeyDown(KeyCode.S) && dir.y != .5f)
            {
                dir = Vector2.down * .5f;
                dirInputs.Add(dir);
                dirSet = true;
            }

           
        }
        
    }

    public bool WillHitWall(Vector3 nextPos)
    {
        // hits top or bottom walls
        if (nextPos.y >= 4.5f || nextPos.y <= -4.5f)
        {
            return true;
        }

        // hits left or right walls
        if (nextPos.x >= 4.5f || nextPos.x <= -4.5f)
        {
            return true;
        }

        return false;

    }

    public bool WillHitBody(Vector3 nextPos)
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (nextPos == bodyParts[i].transform.position)
            {
                return true;
            }
        }
        return false; 
    }

    public List<GameObject> GetBodyParts()
    {
        return bodyParts;
    }

    public static Snake GetSnake()
    {
        return SnakeObject;
    }



}
