/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Gley.MobileAds;

public class AdmobController : MonoBehaviour
{

  
    private void Start()
    {
        API.Initialize(OnInitialized);

    }

    private void OnInitialized()
    {
        API.ShowBanner(BannerPosition.Bottom, BannerType.Adaptive);

        if (!API.GDPRConsentWasSet())
        {
            API.ShowBuiltInConsentPopup(PopupCloseds);
        }
    }

    private void PopupCloseds()
    {

    }

}
