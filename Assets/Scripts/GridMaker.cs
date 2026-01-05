using UnityEngine;

public class GridMaker : MonoBehaviour
{
    public GameObject pixel;
    public Color[] colors;
    public GameObject pixels;

    
    // Update is called once per frame
    void Start()
    {
        int colorToPick = 0;

        for (float y = 4; y >= -4; y-=.5f) 
        { 
            for (float x = -4; x <= 4; x+=.5f)
            {
                Vector3 pos = new Vector3(x, y, 0.0f);
                GameObject newPixel = Instantiate(pixel, pos, Quaternion.identity);
                newPixel.transform.parent = this.transform;
                newPixel.GetComponent<SpriteRenderer>().color = colors[colorToPick % colors.Length];

                colorToPick++;
            }
        }

    }
}
