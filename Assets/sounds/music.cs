using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class music : MonoBehaviour
{

    public AudioSource msc;
    int firstRun = 0;
    public Slider sdvfx;
    public Slider sdmsc;
    public Slider sdsens;
    public Slider dd;
    // Start is called before the first frame update
    void Start()
    {
        firstRun = PlayerPrefs.GetInt("savedFirstRun");
        if (firstRun == 0) // remember "==" for comparing, not "=" which assigns value
        {
            dd.value = 0;
            msc.volume = 1;
            sdvfx.value = 1;
            sdsens.value = 100;
            QualitySettings.SetQualityLevel(0);
            PlayerPrefs.SetFloat("music", 1);
            PlayerPrefs.SetFloat("vfx", 1);
            PlayerPrefs.SetFloat("sens", 100);
            PlayerPrefs.SetInt("savedFirstRun", 1);
            PlayerPrefs.SetInt("graphind", 0);
        }
        else
        {
            dd.value = PlayerPrefs.GetInt("graphind");
            sdvfx.value= PlayerPrefs.GetFloat("vfx");
            sdmsc.value = PlayerPrefs.GetFloat("music");
            sdsens.value = PlayerPrefs.GetFloat("sens");
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("graphind"));
        }
    }

    public void setmusic(float newmusicvalue)
    {

        msc.volume = newmusicvalue;
        PlayerPrefs.SetFloat("music", newmusicvalue);
    }
    public void setvfx(float newvfx)
    {

        
        PlayerPrefs.SetFloat("vfx", newvfx);
    }
    public void setsens(float newsens)
    {


        PlayerPrefs.SetFloat("sens", newsens);
    }

    public void setquality(int graphindex)
    {
        if(graphindex==0)
        {
            QualitySettings.SetQualityLevel(0);
            PlayerPrefs.SetInt("graphind", 0);
        }
        else if (graphindex == 1)
        {
            QualitySettings.SetQualityLevel(1);
            PlayerPrefs.SetInt("graphind", 1);
        }
        else if (graphindex == 2)
        {
            QualitySettings.SetQualityLevel(2);
            PlayerPrefs.SetInt("graphind", 2);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
