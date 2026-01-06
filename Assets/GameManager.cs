using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private int foodCount;

    private void Start()
    {
        instance = this;
    }

    public void IncreaseFoodCount()
    {
        foodCount += 1;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

}
