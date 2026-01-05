using System;
using System.Collections.Generic;
using UnityEngine;


public class Snake : MonoBehaviour
{
    public Vector3 dir;
    public float timer;
    public float interval;
    public Vector3 lastPos;
    public List<GameObject> bodyParts;
    public GameObject body;
    public bool snakeIsDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        snakeIsDead = false;
        bodyParts = new List<GameObject>();
        dir = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        SetSnakeDirection();

        if (timer < interval && dir != Vector3.zero && !snakeIsDead)
        {
            timer = timer + Time.deltaTime;
        } 
        else
        {
            Vector3 nextPos = new Vector3(transform.position.x + dir.x, transform.position.y + dir.y, 0f);
            if ((WillHitWall(nextPos)) && timer >= interval - .5f)
            {
                Debug.Log("Hit a wall");
                snakeIsDead=true;
                return;
            }

            lastPos = transform.position;

            for (int i = 0; i < bodyParts.Count; i++)
            {
                bodyParts[i].GetComponent<Body>().MoveBody();
            }

            transform.position = new Vector3(transform.position.x + dir.x, transform.position.y + dir.y, 0.0f);
            timer = 0.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("food"))
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

            Destroy(collision.gameObject);
        }
    }

    public void SetSnakeDirection()
    {
        
        if (!snakeIsDead)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                dir = Vector2.right * .5f;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                dir = Vector2.left * .5f;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                dir = Vector2.up * .5f;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                dir = Vector2.down * .5f;
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



}
