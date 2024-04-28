using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutsceneHandler : MonoBehaviour
{
    [SerializeField] GameObject heartL;
    [SerializeField] GameObject heartR;

    private void Start()
    {
        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(1);
        LeanTween.moveLocalX(heartL, -240, 0.5f);
        LeanTween.moveLocalX(heartR, -200, 0.5f);
        LeanTween.rotateZ(heartR, -30, 1f);

        yield return null;
    }
}
