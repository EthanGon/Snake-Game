using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private int foodCount;
    [SerializeField] private TextMeshProUGUI foodText;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        
    }

    public void IncreaseFoodCount()
    {
        foodCount += 1;
        foodText.text = "FOOD:" + foodCount;
        Debug.Log("Food Added");
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

}
