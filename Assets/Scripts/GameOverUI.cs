using UnityEngine;
using UnityEngine.UI;
using TMPro;





public abstract class Vehicle
{
    public string name;
}

public class Car : Vehicle
{
    public string model;
}

public class Bike : Vehicle
{
    public int power;
}


public class GameOverUI : MonoBehaviour
{
    // Singleton
    public static GameOverUI Instance { get; private set; }
    
    [SerializeField] private Button restartButton;
    
    [SerializeField] private TextMeshProUGUI messsageText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Awake()
    {


        Vehicle v = new Car();

        
        
        if (Instance != null)
        {
            Debug.LogError("More than one Instance");
        }

        Instance = this;
        
        restartButton.onClick.AddListener(() => {Loader.Load(Loader.Scene.Game);});

        Hide();
    }

    public void Show(bool hasNewHighScore)
    {
        UpdateScoreAndHighScore(hasNewHighScore);
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateScoreAndHighScore(bool hasNewHighScore)
    {
        scoreText.text = Score.GetScore().ToString();
        highScoreText.text = Score.GetHighScore().ToString();
        messsageText.text = hasNewHighScore ? "CONGRATULATIONS!" : "DON'T WORRY, NEXT TIME";

        // if (hasNewHighScore)
        // {
        //     messsageText.text = "CONGRATULATIONS";
        // }
        // else
        // {
        //     messsageText.text = "DON'T WORRY, NEXT TIME";
        // }
    }
}
