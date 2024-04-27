using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject gameOverUI;

    [SerializeField] GameObject dogHead;
    [SerializeField] GameObject coolMeter;
    [SerializeField] GameObject healthMeter;
    [SerializeField] GameObject skull;

    private float cool = 0;
    private float health = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (health < 0) health = 0;
        healthMeter.GetComponent<Image>().fillAmount = health / 3;
    }
}
