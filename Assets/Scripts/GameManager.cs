using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject moveInstruction;
    public GameObject menu;
    public int highScore;
    private bool gameActive;
    private static GameManager instance;
    private int foodCount;
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start()
    {
        highScoreText.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("highscore").ToString();
        gameActive = false;
        instance = this;
    }

    public void IncreaseFoodCount()
    {
        foodCount += 1;
        foodText.text = "FOOD:" + foodCount;
        Debug.Log("Food Added");
    }

    public int GetFoodCount()
    {
        return foodCount;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void StartGame()
    {
        moveInstruction.SetActive(true);
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
