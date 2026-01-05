using UnityEngine;

public class Body : MonoBehaviour
{
    public GameObject next;
    public Vector3 lastPos;

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

}
