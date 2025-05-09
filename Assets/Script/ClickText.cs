using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickText : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform rt;
    float timer = 0;
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        rt.anchoredPosition += Vector2.up * 200 * Time.deltaTime;


        if (timer >= 2f)
        {
            Destroy(this.gameObject);
        }
    }
}
