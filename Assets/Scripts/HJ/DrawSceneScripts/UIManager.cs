using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager;

    public GameObject ui;
    public GameObject back;
    public GameObject palette;
    public GameObject image;

    private void Awake()
    {
        if(uiManager == null)
        {
            uiManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

   


    public void SceneChange()
    {
        SceneManager.LoadScene("DrawScene");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void AppQuit()
    {
        Application.Quit();      
    }

    public void ImageTaking()
    {
        ui.SetActive(false);
        back.SetActive(true);
    }

    public void Back1()
    {
        ui.SetActive(true);
        back.SetActive(false);
    }

    public void PaletteOnOff()
    {
        bool onoff = palette.activeSelf;

        palette.SetActive(!onoff);
        

        //print("????? :" + onoff);
    }

    
}
