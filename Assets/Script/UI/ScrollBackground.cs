using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    public float speed;
    public Vector2 diraction;

    Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        img.material.mainTextureOffset += -diraction.normalized * speed * Time.deltaTime;
    }
}
