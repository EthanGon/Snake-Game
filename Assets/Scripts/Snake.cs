using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Snake : MonoBehaviour
{
    [SerializeField] private float interval;
    public Vector3 dir;
    public Vector3 lastPos;
    public GameObject bodyPrefab;
    public Color eatingColor;
    private static Snake SnakeObject;
    private List<GameObject> bodyParts;
    private float timer;
    private bool snakeIsDead;
    private bool dirSet = false;


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
        if (!GameManager.GetInstance().IsGameActive())
        {
            return;
        } 
        

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
            GameObject newTail = Instantiate(bodyPrefab, lastPos, Quaternion.identity);
            bodyParts.Add(newTail);
            newTail.GetComponent<Body>().SetNext(this.gameObject);
        }
        else
        {
            Vector3 spawnPos = bodyParts[bodyParts.Count - 1].GetComponent<Body>().GetLastPos();

            GameObject newTail = Instantiate(bodyPrefab, spawnPos, Quaternion.identity);
            StartCoroutine(EatingEffect());
            bodyParts.Add(newTail);
            newTail.GetComponent<Body>().SetNext(bodyParts[bodyParts.Count - 2]);
        }
    }

    public IEnumerator EatingEffect()
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            Color ogColor = bodyParts[i].GetComponentInChildren<SpriteRenderer>().color;
            bodyParts[i].GetComponentInChildren<SpriteRenderer>().color = eatingColor;
            yield return new WaitForSeconds(.1f);
            bodyParts[i].GetComponentInChildren<SpriteRenderer>().color = ogColor;
        }
    }

    public void SetSnakeDirection()
    {
        
        if (!snakeIsDead)
        {
            if (Input.GetKeyDown(KeyCode.D) && dir.x != -.5f)
            {
                dir = Vector2.right * .5f;
                dirSet = true;

            }
            else if (Input.GetKeyDown(KeyCode.A) && dir.x != .5f)
            {
                dir = Vector2.left * .5f;
                dirSet = true;
            }
            else if (Input.GetKeyDown(KeyCode.W) && dir.y != -.5f)
            {
                dir = Vector2.up * .5f;
                dirSet = true;
            } 
            else if (Input.GetKeyDown(KeyCode.S) && dir.y != .5f)
            {
                dir = Vector2.down * .5f;
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
