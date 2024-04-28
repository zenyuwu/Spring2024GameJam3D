using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLeft;
    public bool timeOn = false;
    [SerializeField] Text Timertextl;

    void Start()
    {
        timeOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeOn)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
                GameManager.Instance.SetTimeLeft(timeLeft);
            }
            else
            {
                //timer ended
                GameManager.Instance.LoseGame();
                timeLeft = 0;
                timeOn = false;
            }
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        int milliseconds = Mathf.FloorToInt((currentTime - Mathf.Floor(currentTime)) * 1000);

        Timertextl.text = string.Format(" {0:00} : {1:00} : {2:000}", minutes, seconds,milliseconds);
    }
}
