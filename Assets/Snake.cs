using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Vector3 dir;
    public float timer;
    public float interval;
    public Vector3 lastPos;
    public LinkedList<GameObject> list;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        list = new LinkedList<GameObject>();
        list.AddLast(this.gameObject);
        dir = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
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

        

        if (timer < interval && dir != Vector3.zero)
        {
            timer = timer + Time.deltaTime;
        } 
        else
        {
            lastPos = transform.position;
            transform.position = new Vector3(transform.position.x + dir.x, transform.position.y + dir.y, 0.0f);
            timer = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("food"))
        {
            
        }
    }



}
