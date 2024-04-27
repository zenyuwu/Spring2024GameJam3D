using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : Singleton<GameManger>
{
    [SerializeField] GameObject dogHead;
    [SerializeField] GameObject coolMeter;

    private float cool = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cool -= Time.deltaTime;
        coolMeter.GetComponent<Image>().fillAmount = cool / 100;
    }

    public void AddCool(int addedCool)
    {
        cool += addedCool;
        if (cool > 100) cool = 100; 
        coolMeter.GetComponent<Image>().fillAmount = cool / 100;
    }
}
