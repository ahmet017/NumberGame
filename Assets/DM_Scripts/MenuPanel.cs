/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
using UnityEngine;
using UnityEngine.UI;
using Gley.GameServices;
public class MenuPanel : BasePanel
{
	protected new void Start()
	{
		base.Start();
		GameUser instance = GameUser.Instance;
	
		UIManager.selfInstance.bkgPanel.SelectTheme(instance.nowTheme);

#if GLEY_GAMESERVICES_ANDROID
		if (!API.IsLoggedIn())
		{
			API.LogIn(LoginResult);
		}
#endif
	}



#if GLEY_GAMESERVICES_ANDROID
	//Automatically called when Login is complete 
	private void LoginResult(bool success)
	{
		if (success == true)
		{
			//Login was successful
		}
		else
		{
			//Login failed
		}
		Debug.Log("Login success: " + success);
	}
#endif

#if GLEY_GAMESERVICES_ANDROID
	public void LeaderBoardClick()
	{

		//Login to Game Servicies

		if (!API.IsLoggedIn())
		{
			API.LogIn(LoginResult);
		}
		else
		{
			API.ShowLeaderboadsUI();
		}

	}
#endif


	protected new void OnEnable()
	{
		base.OnEnable();
		this.alreadyShowNgs = false;
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
	
	}

	public void OnStart()
	{
		AudioSystem.PlayOneShotEffect("btn");
		base.NextPanel(UIManager.selfInstance.gamePanel.gameObject);
	}

	public void OnSetting()
	{
		AudioSystem.PlayOneShotEffect("btn");
		base.NextPanel(UIManager.selfInstance.settingPanel.gameObject);
	}

    private new void Update()
    {
        
    }

    public void OnAnimInAfter()
	{
		if (!this.firstIn)
		{
			this.firstIn = true;
			if (Storage.ReadConfig("refusenotification", "false") == "false")
			{
				//this.RequestLocalNotice();
			}
		}
		
	}


    public void ShowIapPanel()
    {
        if (this.isEnd)
        {
            return;
        }
        AudioSystem.PlayOneShotEffect("btn");
    
        iapPanel.SetActive(true);
    }

    public void HideIapPanel()
    {
        if (this.isEnd)
        {
            return;
        }
        AudioSystem.PlayOneShotEffect("btn");
        iapPanel.SetActive(false);
    }


	public void HideSettingPanel()
	{
		//Debug.Log("Chay vao day");
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		settingPannel.SetActive(false);
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

    public void OnRate()
    {
        if (this.isEnd)
        {
            return;
        }
        AudioSystem.PlayOneShotEffect("btn");
        UIManager.selfInstance.ratePanel.gameObject.SetActive(true);
    }

    private void Awake()
    {
        instance = this;
    }


	public GameObject commonLogo;

	private bool firstIn;

	private bool alreadyShowNgs;

    public GameObject soundOn;

    public GameObject soundOff;

    public GameObject enNoAds;

    public GameObject rateGo;

    public GameObject iapPanel;

	public GameObject settingPannel;

    public static MenuPanel instance;
}
