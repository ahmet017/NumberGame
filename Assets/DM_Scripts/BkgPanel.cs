/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BkgPanel : BasePanel
{
	protected new void Start()
	{
		base.Start();
        StartCoroutine(CheckVideoRoutine());
	}

	protected new void OnEnable()
	{
		base.OnEnable();
	}

	protected new void OnDisable()
	{
		base.OnDisable();
	}

	public void SelectTheme(int index)
	{
		for (int i = 0; i < this.bkgImgs.Length; i++)
		{
			this.bkgImgs[i].sprite = this.themeSprite[index];
		}

    }

	IEnumerator CheckVideoRoutine()
    {
        if (Base._instance.IsRewardReady())
        {
            notiGo.gameObject.SetActive(true);
        }
        else
        {
            notiGo.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(10);
        StartCoroutine(CheckVideoRoutine());
    }

	public Image[] bkgImgs;


	public int themeCount;

	public Sprite[] themeSprite;


	private float targetPec;

    public GameObject notiGo;
}
