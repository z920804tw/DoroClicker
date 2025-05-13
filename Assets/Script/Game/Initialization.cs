using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    void Awake()
    {
        if (!PlayerPrefs.HasKey("Initialization"))
        {
            PlayerPrefs.SetInt("Initialization", 1);

            //解析度初始化
            PlayerPrefs.SetInt("ScreenResolutionX", Screen.currentResolution.width);
            PlayerPrefs.SetInt("ScreenResolutionY", Screen.currentResolution.height);
            PlayerPrefs.SetInt("FullScreen", 1);

            //音量初始化
            PlayerPrefs.SetFloat("MainVolume", 1);
            PlayerPrefs.SetFloat("GameVolume", 0.7f);
            PlayerPrefs.SetFloat("SFXVolume", 0.7f);
            Debug.Log("初始化完成");
        }
    }
}
