/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Gley.Localization;

public class RatePanel : PopupController
{

    public Transform[] starBtn;

    public Sprite ratedSpr;

    public Transform laterBtn;

    public Transform thanksContainer;

    public Text title, des1, des2, later;

    public override void OnEnable()
    {
        base.OnEnable();

        if(title != null)
        {
            title.text = API.GetText(WordIDs.RatePanel_title);
        }

        if(des1 != null)
        {
            des1.text = API.GetText(WordIDs.RatePanel_des1);
        }

        if (des2 != null)
        {
            des2.text = API.GetText(WordIDs.RatePanel_des2);
        }

        if (later != null)
        {
            later.text = API.GetText(WordIDs.RatePanel_later);
        }
        //PlayerPrefs.DeleteKey("rated"); 
        if (PlayerPrefs.HasKey("rated"))
        {
            thanksContainer.gameObject.SetActive(true);
            laterBtn.gameObject.SetActive(false);
            for (int i = 0; i < PlayerPrefs.GetInt("Rated"); i++)
            {
                starBtn[i].GetComponent<Image>().sprite = ratedSpr;
            }
        }
        else
        {
            laterBtn.gameObject.SetActive(true);
            thanksContainer.gameObject.SetActive(false);
        }
    }

    public void Rate(int index)
    {
        if (!PlayerPrefs.HasKey("rated"))
        {
            for (int i = 0; i <= index; i++)
            {
                starBtn[i].GetComponent<Image>().sprite = ratedSpr;
            }

            if (index < 4)
            {
                laterBtn.gameObject.SetActive(false);
                thanksContainer.gameObject.SetActive(true);
                PlayerPrefs.SetInt("rated", index);
            }
            else
            {
                Application.OpenURL("market://details?id=" + Application.identifier);
                PlayerPrefs.SetInt("rated", index);
                laterBtn.gameObject.SetActive(false);
                thanksContainer.gameObject.SetActive(true);
            }
            StartCoroutine(CloseRoutine());
        }
    }

    IEnumerator CloseRoutine()
    {
        yield return new WaitForSeconds(1);
        ClosePopup();
    }
}
