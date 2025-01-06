/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
using UnityEngine;
using UnityEngine.UI;
using Gley.MobileAds;

public class PausePanel : BasePanel
{
	protected new void Start()
	{
		base.Start();
	}

	protected new void OnEnable()
	{
		base.OnEnable();

		pauseTitle.text = Gley.Localization.API.GetText(Gley.Localization.WordIDs.Pause_title);
   
        soundOn.SetActive(AudioSystem.isSound);
        soundOff.SetActive(!AudioSystem.isSound);

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
		this.OnResume();
	}

	public void AfterAnimIn()
	{
//		UIManager.selfInstance.VAinstance.ShowNGS(false);
	}

	public void OnBack()
	{
		//AdsInitScript._instance.gameStatus = GameState.GoToHome;
		API.ShowInterstitial();
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		this.PANEL_OUT = 3;
		this.PANEL_IN = 0;
		UIManager.selfInstance.gamePanel.OnClose();
		base.NextPanel(UIManager.selfInstance.menuPanel.gameObject);
	}

	public void OnResume()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		this.PANEL_OUT = 1;
		this.PANEL_IN = 0;
		base.NextFunction(delegate
		{
			base.gameObject.SetActive(false);
			UIManager.selfInstance.gamePanel.OnResume();
		});
	}

	public void OnRestart()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		this.PANEL_OUT = 1;
		this.PANEL_IN = 0;
		base.NextFunction(delegate
		{
			base.gameObject.SetActive(false);
			UIManager.selfInstance.gamePanel.OnRestart();
		});
	}

	public void OnSetting()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		this.PANEL_OUT = 3;
		this.PANEL_IN = 2;
		base.NextPanel(UIManager.selfInstance.settingPanel.gameObject);
	}

	public void OnOtherGame()
	{
	
	}

	public void OnRemoveAds()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		UIManager.selfInstance.noAdsThisTime = true;
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

	public Image imgNoAds;

	public Text pauseTitle;

	private int useSpIndex;

    public GameObject soundOn;

    public GameObject soundOff;
}
