using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    private bool gameActive;
    private static GameManager instance;
    [SerializeField] private int foodCount;
    [SerializeField] private TextMeshProUGUI foodText;

    private void Start()
    {
        gameActive = false;
        instance = this;
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

    public void StartGame()
    {
        gameActive=true;
        menu.SetActive(false);
        FoodSpawner.GetInstance().SpawnFood();
    }

    public void ShowMenu()
    {
        gameActive = false;
        menu.SetActive(true);
    }

    public bool IsGameActive()
    {
        return gameActive;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
