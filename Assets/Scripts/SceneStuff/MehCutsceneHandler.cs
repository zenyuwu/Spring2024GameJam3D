using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MehCutsceneHandler : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Start()
    {
        text.text = "time left: " + GameManager.Instance.timeLeft;
    }
}
