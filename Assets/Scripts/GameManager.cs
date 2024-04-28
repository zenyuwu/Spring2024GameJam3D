using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] SceneHandler sceneHandler;

    [SerializeField] GameObject coolMeter;
    [SerializeField] GameObject healthMeter;
    [SerializeField] GameObject skull;

    private float cool = 0;
    private float health = 0;
    private bool gameLost = false;
    public float timeLeft = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitDog();
    }

    public void InitDog()
    {
        coolMeter = GameObject.Find("CoolMask");
        healthMeter = GameObject.Find("DogMask");
        skull = GameObject.Find("Skull");
        sceneHandler = GameObject.Find("SceneManager").GetComponent<SceneHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameLost && health < 0)
        {
            LoseGame();
        }
    }

    public void AddCool(float addedCool)
    {
        cool += addedCool;
        if (cool > 100) cool = 100; 
        coolMeter.GetComponent<Image>().fillAmount = cool / 100;
    }

    public void SetCool(float newCool)
    {
        cool = newCool;
        if (cool > 100) { cool = 100; }
        coolMeter.GetComponent<Image>().fillAmount = cool / 100;
        if (cool > 80) { skull.SetActive(true); } else { skull.SetActive(false); }
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
        if (health > 3) health = 3;
        healthMeter.GetComponent<Image>().fillAmount = health / 3;
    }

    public void WinGame()
    {
        if(timeLeft > 30)
        {
            sceneHandler.OpenGameWon();
        }
        else
        {
            sceneHandler.OpenGameMeh();
        }
    }

    public void LoseGame()
    {
        gameLost = true;
        sceneHandler.OpenGameLost();
    }

    public void SetTimeLeft(float newTime)
    {
        timeLeft = newTime;
    }
}
