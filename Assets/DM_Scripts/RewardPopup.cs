/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gley.Localization;

public class RewardPopup : MonoBehaviour
{
    public Text bodyText, title;

    public Image tipImg;

    public Sprite removeSpr;

    public Sprite lightSpr;

    public Sprite undoSpr;

    private void OnEnable()
    {
        if(title != null)
        {
            title.text = API.GetText(WordIDs.Reward_title);
        }
        switch (Base._instance.randomRewardindex)
        {
            case 0:
                tipImg.sprite = removeSpr;
                //bodyText.text = "You've Got " + Base._instance.randomRewardCount + " Remove Block Tips";
                bodyText.text = string.Format(API.GetText(WordIDs.Reward_des_remove), Base._instance.randomRewardCount);
                
                break;
            case 1:
                tipImg.sprite = lightSpr;
                //bodyText.text = "You've Got " + Base._instance.randomRewardCount + " Light Hint Tips";
                bodyText.text = string.Format(API.GetText(WordIDs.Reward_des_hint), Base._instance.randomRewardCount);
                break;
            case 2:
                tipImg.sprite = undoSpr;
                //bodyText.text = "You've Got " + Base._instance.randomRewardCount + " Undo Block Tips";
                bodyText.text = string.Format(API.GetText(WordIDs.Reward_des_undo) , Base._instance.randomRewardCount);
                break;
        }
    }
}
