/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.Localization;
using UnityEngine.UI;

public class IAPUI : MonoBehaviour
{
    public Text titlePanel, removeads, video, desRemove, desHint, desUndo;
    // Start is called before the first frame update

    private void OnEnable()
    {
        //Debug.Log("Chay vao IAPUI");
        if (titlePanel != null)
        {
            titlePanel.text = API.GetText(WordIDs.IAP_title);
        }

        if (removeads != null)
        {
            removeads.text = API.GetText(WordIDs.IAP_removeads);
        }

        if(desRemove != null)
        {
            desRemove.text = API.GetText(WordIDs.IAP_des_remove);
        }

        if (desUndo != null)
        {
            desUndo.text = API.GetText(WordIDs.IAP_des_undo);
        }

        if (desHint != null)
        {
            desHint.text = API.GetText(WordIDs.IAP_des_hint);
        }

        if(video != null)
        {
            video.text = API.GetText(WordIDs.IAP_video);
        }
    }

}
