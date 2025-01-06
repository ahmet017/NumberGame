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

public class PassPanel : BasePanel
{
	protected new void Start()
	{
		base.Start();
	}

	protected new void OnEnable()
	{
		base.OnEnable();
		long targetScore = UIManager.selfInstance.gamePanel.targetScore;
		if (UIManager.selfInstance.prePanel == UIManager.selfInstance.gamePanel)
		{
			GameUser instance = GameUser.Instance;
			long bestRecord = instance.bestRecord;
			if (targetScore > bestRecord)
			{
				this.labTitle.text = Localization.getLocalString(this.praises[UnityEngine.Random.Range(0, 3)], "AWESOME!");
				instance.bestRecord = targetScore;
				this.labBest.gameObject.SetActive(false);
			}
			else
			{
				this.labTitle.text = Localization.getLocalString(this.praises[3], "AWESOME!");
				this.labBest.text = Localization.getLocalString("txtBest", "Best") + ":" + bestRecord.ToString("N0");
				this.labBest.gameObject.SetActive(true);
			}
			this.isEnd = true;
			this.labTitle.gameObject.SetActive(false);
			GameUser.Instance.ClearLastGameData();
			GameUser.Save();
			string text = "";
		
			base.StopCoroutine("GrowTime");
			base.StartCoroutine("GrowTime");
		}
		else if (UIManager.selfInstance.prePanel == UIManager.selfInstance.settingPanel)
		{
			this.isEnd = false;
			this.labTitle.gameObject.SetActive(true);
			this.labTitle.GetComponent<Animator>().enabled = false;
			this.labNow.text = targetScore.ToString("N0");
			this.AdOrRate();
		}
	
		UIManager.selfInstance.topPanel.SwitchThemeBtn(true);
		KeyBoardListener kbl = UIManager.selfInstance.kbl;
		kbl.onBackKeyEvent = (KeyBoardListener.OnBackKeyEvent)Delegate.Combine(kbl.onBackKeyEvent, new KeyBoardListener.OnBackKeyEvent(this.OnBackKeyEvent));
	}

	protected new void OnDisable()
	{
		base.OnDisable();
		KeyBoardListener kbl = UIManager.selfInstance.kbl;
		kbl.onBackKeyEvent = (KeyBoardListener.OnBackKeyEvent)Delegate.Remove(kbl.onBackKeyEvent, new KeyBoardListener.OnBackKeyEvent(this.OnBackKeyEvent));
	}

	private void OnBackKeyEvent()
	{
		if (!UIManager.selfInstance.ratePanel.gameObject.activeSelf)
		{
			this.OnClose();
		}
	}

	private IEnumerator GrowTime()
	{
		AudioSystem.PlayOneShotEffect("levelcomplete");
		this.labNow.text = "0";
		long cacheRecord = UIManager.selfInstance.gamePanel.targetScore;
		yield return new WaitForSeconds(0.2f);
		AudioSystem.PlayOneShotEffect("sfx_score");
		for (int i = 0; i <= 60; i++)
		{
			long useRecord = (long)((double)(cacheRecord * (long)i) * 1.0 / 60.0);
			this.labNow.text = useRecord.ToString("N0");
			yield return new WaitForEndOfFrame();
		}
		this.labNow.text = cacheRecord.ToString("N0");
		this.labTitle.gameObject.SetActive(true);
		this.labTitle.GetComponent<Animator>().enabled = true;
		yield return new WaitForSeconds(0.1f);
		this.isEnd = false;
		this.AdOrRate();
		yield break;
	}

	private void AdOrRate()
	{
		GameUser instance = GameUser.Instance;
		this.cacheGameAdRet++;
		if (/*!instance.alreadyRate && UIManager.selfInstance.VAinstance.adData.isReview && this.cacheGameAdRet >= UIManager.selfInstance.VAinstance.adData.reviewTim*/!PlayerPrefs.HasKey("rated"))
		{
            //UIManager.selfInstance.ratePanel.gameObject.SetActive(true);
            //instance.alreadyRate = true;
            //GameUser.Save();
            UIManager.selfInstance.ratePanel.gameObject.SetActive(true);
		}
		else
		{
//			UIManager.selfInstance.VAinstance.ShowNGS(false);
		}
	}

	public void OnClose()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		base.NextPanel(UIManager.selfInstance.menuPanel.gameObject);
	}

	public void OnMoreGame()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");

	}

	public void OnShare()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		string text = utils.GetWritablePath(string.Empty) + "/share.png";
	
	}

	public void OnRank()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		if (!InitScript.isLoginGameCenter)
		{
			//VectorNative.invokeNative(128, string.Empty);
			InitScript.isLoginGameCenter = true;
		}
		if (InitScript.isLoginGameCenter)
		{
			string text = "";
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				text = "";
			}
			long num = GameUser.Instance.bestRecord;
			if (UIManager.selfInstance.gamePanel.targetScore > num)
			{
				num = UIManager.selfInstance.gamePanel.targetScore;
			}
			
		}
	}

	public void OnRemoveAds()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		UIManager.selfInstance.noAdsThisTime = true;
	//	VectorAds.invokeAds(109, "pass_noads");
//		VectorNative.invokeNative(112, "nlhexa_noads");
	}

	public void OnGoOn()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		base.NextPanel(UIManager.selfInstance.gamePanel.gameObject);
	}

	public void OnSetting()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		base.NextPanel(UIManager.selfInstance.settingPanel.gameObject);
	}

	private new void Update()
	{
	}

	public Text labTitle;

	public Text labNow;

	public Text labBest;

	public Image imgNoAds;

	private int cacheGameAdRet;

	private readonly string[] praises = new string[]
	{
		"txtPass0",
		"txtPass1",
		"txtPass2",
		"txtPass3"
	};
}
