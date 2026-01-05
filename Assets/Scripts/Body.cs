using UnityEngine;

public class Body : MonoBehaviour
{
    public GameObject next;
    public Vector3 lastPos;

    public void MoveBody()
    {
        this.lastPos = transform.position;
        
        if (next.CompareTag("head"))
        {
            transform.position = next.gameObject.GetComponent<Snake>().lastPos;
        } 
        else
        {
            transform.position = next.GetComponent<Body>().lastPos;
        }
    }

}
