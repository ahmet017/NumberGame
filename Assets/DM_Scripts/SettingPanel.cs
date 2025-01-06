/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
//using com.vector;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
	protected new void Start()
	{
		base.Start();
		if (AudioSystem.isSound)
		{
			this.soundOn.SetActive(true);
			this.soundOff.SetActive(false);
		}
		else
		{
			this.soundOn.SetActive(false);
			this.soundOff.SetActive(true);
		}
		
	}

	protected new void OnEnable()
	{
		base.OnEnable();

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
		this.OnClose();
	}

	public void OnAnimInAfter()
	{
		
	}

	public void OnClose()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		base.NextPanel(UIManager.selfInstance.prePanel.gameObject);
	}

	public void OnSoundSwitch(bool isOpen)
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.SwitchSound(isOpen);
		if (AudioSystem.isSound)
		{
			this.soundOn.SetActive(true);
			this.soundOff.SetActive(false);
		}
		else
		{
			this.soundOn.SetActive(false);
			this.soundOff.SetActive(true);
		}
		AudioSystem.PlayOneShotEffect("btn");
	}

	public void OnNotificationSwitch(bool isOpen)
	{

	}

	public void OnRate()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");

	}


	public void OnRank()
	{
		Debug.Log("SCORE :" + GameUser.Instance.lastRecord);
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

	public void OnMoreGame()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		
	}

	

	

	private new void Update()
	{
	}

	public GameObject soundOn;

	public GameObject soundOff;




	public GameObject rateGo;

	internal const string ANDROID_LEADERBOARD = "";

	internal const string IOS_LEADERBOARD = "";

	internal const string IAP_NOADS = "";
}
