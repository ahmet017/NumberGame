using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Gley.MobileAds;

public class AdsInitScript : MonoBehaviour
{
   

    public void CheckDaily()
    {
        int num = PlayerPrefs.GetInt("dailyDay", 1);
        DateTime dateTime = DateTime.Now;
        if (!PlayerPrefs.HasKey("SaveDate"))
        {
            PlayerPrefs.SetString("SaveDate", dateTime.ToBinary().ToString());
        }
        else
        {
            DateTime d = DateTime.FromBinary(long.Parse(PlayerPrefs.GetString("SaveDate")));
            num = dateTime.Day - d.Day;
        }
        if (!PlayerPrefs.HasKey("daily" + num.ToString()))
        {
            UnityEngine.Debug.Log("day : " + num);
            PlayerPrefs.SetInt("dailyDay", num);
            PlayerPrefs.SetInt("daily" + num.ToString(), num);
            PlayerPrefs.Save();
        }

    }
}
