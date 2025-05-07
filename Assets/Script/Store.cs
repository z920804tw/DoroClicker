using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject[] pages;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenPage(int i)
    {
        foreach (GameObject x in pages)
        {
            x.SetActive(false);
        }
        pages[i].SetActive(true);
    }
}
