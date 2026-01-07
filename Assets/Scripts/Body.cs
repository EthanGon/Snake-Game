using UnityEngine;

public class Body : MonoBehaviour
{
    private GameObject next;
    private Vector3 lastPos;

    public void MoveBody()
    {
        // before it moves save the last position, then move to the next body parts last position.
        this.lastPos = transform.position;
        
        if (next.CompareTag("head"))
        {
            transform.position = next.GetComponent<Snake>().lastPos;
        } 
        else
        {
            transform.position = next.GetComponent<Body>().lastPos;
        }
    }

    public Vector3 GetLastPos()
    {
        return this.lastPos;
    }

    public void SetNext(GameObject next)
    {
        this.next = next;
    }

    public GameObject GetNext()
    {
        return this.next;
    }

}
