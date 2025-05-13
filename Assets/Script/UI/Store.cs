using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject[] pages;


    float timer;


    public void OpenPage(int i)
    {
        foreach (GameObject x in pages)
        {
            x.SetActive(false);
        }
        pages[i].SetActive(true);
    }

    public void OpenAndClosePage()
    {
        StartCoroutine(UiAnim());
    }

    IEnumerator UiAnim()
    {
        timer = 0;
        float duration = 0.5f;
        RectTransform rt= GetComponent<RectTransform>();
        Vector3 startPos= rt.anchoredPosition;
        while (timer <= duration)
        {
            timer+=Time.deltaTime;
            float t= timer/duration;
            rt.anchoredPosition=Vector3.Lerp(startPos,-startPos,t);
            yield return null;
        }

        rt.anchoredPosition=-startPos;

    }
}
