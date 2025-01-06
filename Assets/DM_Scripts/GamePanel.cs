/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Gley.MobileAds;
using Gley.GameServices;

public class GamePanel : BasePanel
{
	protected new void Start()
	{
		base.Start();
	}

	protected new void OnEnable()
	{
		this.txtGuide.gameObject.SetActive(true);
		this.cancelGo.SetActive(false);
		base.OnEnable();
		this.gameGo.SetActive(true);
		this.UpdateLevel();
		Vector2 sizeDelta = Game.main.GetComponent<RectTransform>().sizeDelta;
		Vector2 anchoredPosition = this.topRect.anchoredPosition;
		anchoredPosition.y = sizeDelta.y / 2f + this.topRect.sizeDelta.y / 2f;
		//this.topRect.anchoredPosition = anchoredPosition;
		anchoredPosition = this.bottomRect.anchoredPosition;
		anchoredPosition.y = -sizeDelta.y / 2f - this.bottomRect.sizeDelta.y / 2f;
		this.bottomRect.anchoredPosition = anchoredPosition;
		float num = UIManager.selfInstance.canvas.GetComponent<RectTransform>().sizeDelta.y * 0.5f;
		float y = num + anchoredPosition.y - 20f;
		UIManager.selfInstance.topPanel.UpdateThemeBtn(y);
		UIManager.selfInstance.topPanel.SwitchThemeBtn(true);
        this.UpdateTipStatus1();
        KeyBoardListener kbl = UIManager.selfInstance.kbl;
		kbl.onBackKeyEvent = (KeyBoardListener.OnBackKeyEvent)Delegate.Combine(kbl.onBackKeyEvent, new KeyBoardListener.OnBackKeyEvent(this.OnBackKeyEvent));
        Base._instance.UpdateCount();
	}

	

	private void UpdateTipStatus1()
	{
		this.tipImg.gameObject.SetActive(false);
		this.lightGo.SetActive(true);
		this.removeGo.SetActive(true);
		this.undoGo.SetActive(true);
		this.cancelGo.SetActive(false);
		if (Game.main.undoDotList == null)
		{
            //this.undoImg.color = new Color(0.5f, 0.5f, 0.5f, 1); // this.deactiveColor;
            undoImg.sprite = undoDisable;
        }
		else
		{
            //this.undoImg.color = Color.white; // this.yellowActiveColor;
            undoImg.sprite = undoEnable;
		}
        Base._instance.UpdateCount();
    }

	private void UpdateTipStatus2()
	{
		this.tipImg.gameObject.SetActive(false);
		this.lightGo.SetActive(false);
		this.removeGo.SetActive(false);
		this.undoGo.SetActive(false);
		this.cancelGo.SetActive(true);
        Base._instance.UpdateCount();
    }

	protected new void OnDisable()
	{
		base.OnDisable();
		this.gameGo.SetActive(false);
		UIManager.selfInstance.topPanel.UpdateThemeBtn(82f);
		KeyBoardListener kbl = UIManager.selfInstance.kbl;
		kbl.onBackKeyEvent = (KeyBoardListener.OnBackKeyEvent)Delegate.Remove(kbl.onBackKeyEvent, new KeyBoardListener.OnBackKeyEvent(this.OnBackKeyEvent));
	}

	private void OnBackKeyEvent()
	{
		this.OnPause();
	}

	private void UpdateLevel()
	{
		this.guideHand.gameObject.SetActive(false);
		this.txtGuide.gameObject.SetActive(false);
		this.labNow.gameObject.SetActive(true);
		GameUser instance = GameUser.Instance;
		//this.costTimeGo.SetActive(true);
		this.nowScore = instance.lastRecord;
		this.targetScore = this.nowScore;
		//long bestRecord = instance.bestRecord;
		//if(instance.lastRecord > bestRecord)
  //      {
		//	instance.bestRecord = instance.lastRecord;
		//}
		this.labNow.text = this.targetScore.ToString("N0");
		this.txtBestScore.text = instance.bestRecord.ToString();
		this.costTim = instance.lastTime;
		this.labCostTim.text = ((int)this.costTim / 60).ToString((this.costTim / 60f < 10f) ? "D2" : string.Empty) + ":" + ((int)this.costTim % 60).ToString("D2");
		Game.main.GameInit();
	}

	internal void AddScore(long addNum)
	{
		this.targetScore += addNum;
	}

	internal void DelayNgs(float delayTim)
	{
		base.CancelInvoke("InvokeAds");
		base.Invoke("InvokeAds", delayTim);
	}

	private void InvokeAds()
	{
//		UIManager.selfInstance.VAinstance.ShowNGS(false);
	}

	internal void UpdateGuideHand(bool isShow, Vector2 newPos = default(Vector2))
	{
		this.guideHand.gameObject.SetActive(isShow);
		if (isShow)
		{
			this.txtGuide.gameObject.SetActive(true);
			this.labNow.gameObject.SetActive(false);
		}
		else
		{
			this.txtGuide.gameObject.SetActive(false);
			this.labNow.gameObject.SetActive(true);
		}
		this.guideHand.anchoredPosition = newPos;
	}

	internal void GameFinish()
	{
		this.isEnd = true;
		base.StartCoroutine("GoToPassPanel");
	}

	private IEnumerator GoToPassPanel()
	{
		yield return new WaitForSeconds(0.1f);
		string filePath = utils.GetWritablePath(string.Empty) + "/share.png";
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
		{
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
			ScreenCapture.CaptureScreenshot("share.png");
		}
		else
		{
			if (!Directory.Exists(utils.GetWritablePath(string.Empty)))
			{
				Directory.CreateDirectory(utils.GetWritablePath(string.Empty));
			}
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
			ScreenCapture.CaptureScreenshot(filePath);
		}
		yield return new WaitForSeconds(0.5f);
		if (GameUser.Instance.isNewGame && Base._instance.IsRewardReady())
		{
			GameUser.Instance.isNewGame = false;
			GameUser.Save();
			UIManager.selfInstance.revivePanel.gameObject.SetActive(true);
		}
		else
		{
			this.ToPassPanel();
		}
		yield break;
	}

	public void ToPassPanel()
	{
		Gley.MobileAds.API.ShowInterstitial();
		this.isEnd = false;
		base.NextPanel(UIManager.selfInstance.passPanel.gameObject);
	}

	public void GameRevive()
	{
		this.isEnd = false;
		Game.main.Revive();
	}

	public void OnClose()
	{
		this.isEnd = true;
		base.gameObject.SetActive(false);
	}

	public void OnPause()
	{
		if (this.isEnd)
		{
			return;
		}
		this.isEnd = true;
		AudioSystem.PlayOneShotEffect("btn");
		GameUser.Instance.SaveLastGameData();
		UIManager.selfInstance.pausePanel.gameObject.SetActive(true);
		Game.main.status = Game.PLAYSTATUS.STOP;
#if GLEY_GAMESERVICES_ANDROID
		Debug.Log("SUMMIT SCRORE TO LB : " + GameUser.Instance.bestRecord);
		Gley.GameServices.API.SubmitScore(GameUser.Instance.bestRecord, LeaderboardNames.NumberGame_LB, ScoreSubmitted);
#endif
	}

#if GLEY_GAMESERVICES_ANDROID || GLEY_GAMESERVICES_IOS
	//Automatically called when a score was submitted 
	private void ScoreSubmitted(bool success, GameServicesError error)
	{
		if (success)
		{
			//score successfully submitted
		}
		else
		{
			//an error occurred
			Debug.LogError("Score failed to submit: " + error);
		}
		Debug.Log("Submit score result: " + success + " message:" + error);
	}
#endif

	public void OnResume()
	{
		this.isEnd = false;
		UIManager.selfInstance.topPanel.SwitchThemeBtn(true);
		Game.main.status = Game.PLAYSTATUS.RUN;
	}

    public void OnTips()
    {
        if (this.isEnd || Game.main.isGuide || (long)utils.CurrentSeconds() < GameUser.Instance.lastTipTimeCount)
        {
            return;
        }
    
       
    }

	public void InvokeAdsCallback(int type, string msg)
	{
		if (type != 1)
		{
			if (type == 0)
			{
				this.UpdateTipStatus1();
				//VectorAdsInstance vainstance = UIManager.selfInstance.VAinstance;
				//vainstance.mCallBack = (VectorAdsInstance.InvokeAdsCallback)Delegate.Remove(vainstance.mCallBack, new VectorAdsInstance.InvokeAdsCallback(this.InvokeAdsCallback));
			}
		}
		else
		{
			if (msg == "true")
			{
				this.UpdateTipStatus1();
			}
			//VectorAdsInstance vainstance2 = UIManager.selfInstance.VAinstance;
			//vainstance2.mCallBack = (VectorAdsInstance.InvokeAdsCallback)Delegate.Remove(vainstance2.mCallBack, new VectorAdsInstance.InvokeAdsCallback(this.InvokeAdsCallback));
		}
	}

	public void OnLight()
	{
		if (this.isEnd)
		{
			return;
		}
        if(RewardScriptableObject.instance.tipLightCount > 0)
        {
            AudioSystem.PlayOneShotEffect("btn");
            Game.main.TipLight();
            RewardScriptableObject.instance.tipLightCount--;
            this.UpdateTipStatus1();
        }
		else
		{
			AudioSystem.PlayOneShotEffect("btn");
			iapPannel.SetActive(true);
			Debug.Log("Open Shop Pannel");
		}
	}

	public void OnRemove()
	{
		if (this.isEnd)
		{
			return;
		}
        if (RewardScriptableObject.instance.tipRemoveCount > 0)
        {
            AudioSystem.PlayOneShotEffect("btn");
            Game.main.TipRemove();
            this.UpdateTipStatus2();
        }
        else
        {
			AudioSystem.PlayOneShotEffect("btn");
			iapPannel.SetActive(true);
			Debug.Log("Open Shop Pannel");
        }
	}

    public void OnUndo()
	{
		if (this.isEnd || Game.main.undoDotList == null)
		{
			return;
		}
        if (RewardScriptableObject.instance.tipUndoCount > 0)
        {
            AudioSystem.PlayOneShotEffect("btn");
            Game.main.TipUndo();
			//RewardScriptableObject.instance.tipUndoCount--;
		}
		else
		{
			AudioSystem.PlayOneShotEffect("btn");
			iapPannel.SetActive(true);
			Debug.Log("Open Shop Pannel");
		}
	}

	public void FinishRemove()
	{
        //this.UpdateTipCounts();
        UpdateTipStatus1();
    }

	

	public void OnCancel()
	{
		if (this.isEnd)
		{
			return;
		}
		AudioSystem.PlayOneShotEffect("btn");
		Game.main.ClearTipStatus();
        //this.UpdateTipCounts();
        UpdateTipStatus1();
        Base._instance.UpdateCount();
	}

	public void OnRestart()
	{
		this.isEnd = false;
		GameUser.Instance.ClearLastGameData();
		GameUser.Save();
		this.UpdateLevel();
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus && Game.main.status != Game.PLAYSTATUS.STOP)
		{
			GameUser.Instance.SaveLastGameData();
			Debug.Log("SUMMIT SCRORE TO LB" + GameUser.Instance.lastRecord);
		}
	}

	private new void Update()
	{
		if (Game.main.status == Game.PLAYSTATUS.RUN)
		{
			this.costTim += Time.deltaTime;
			this.labCostTim.text = ((int)this.costTim / 60).ToString((this.costTim / 60f < 10f) ? "D2" : string.Empty) + ":" + ((int)this.costTim % 60).ToString("D2");
			this.videoCheckTime += Time.deltaTime;
			if (this.videoCheckTime >= 5f && !this.isVideoCache)
			{
				this.videoCheckTime = 0f;
                //if (GameUser.Instance.isNoAds || VectorAds.invokeAds(106, string.Empty) == "true" || VectorAds.invokeAds(104, string.Empty) == "true")
                //{
                if (Base._instance.IsRewardReady())
                {
                    this.isVideoCache = true;
                    this.tipImg.color = this.pinkActiveColor;
                }
				//}
				//else
				//{
				//	this.isVideoCache = false;
				//	this.tipImg.color = this.deactiveColor;
				//}
			}
			if (GameUser.Instance.lastTipTimeCount == 0L || (long)utils.CurrentSeconds() > GameUser.Instance.lastTipTimeCount)
			{
				if (this.isVideoCache)
				{
					this.emptyTouchTime += Time.deltaTime;
				}
				this.labTipCount.text = string.Empty;
				this.tipStatus0.gameObject.SetActive(this.emptyTouchTime < 15f);
				this.tipStatus1.gameObject.SetActive(this.emptyTouchTime >= 15f);
			}
			else
			{
				long num = GameUser.Instance.lastTipTimeCount - (long)utils.CurrentSeconds();
				this.labTipCount.text = ((int)num / 60).ToString((num / 60L < 10L) ? "D2" : string.Empty) + ":" + ((int)num % 60).ToString("D2");
				this.tipStatus0.gameObject.SetActive(false);
				this.tipStatus1.gameObject.SetActive(false);
			}
			if (Game.main.undoDotList == null)
			{
                //this.undoImg.color = new Color(0.5f, 0.5f, 0.5f, 1); //this.deactiveColor;
                undoImg.sprite = undoDisable;
            }
			else
			{
                //this.undoImg.color = Color.white; //this.yellowActiveColor;'
                undoImg.sprite = undoEnable;
			}
		}
		if (this.nowScore != this.targetScore)
		{
			long num2 = (long)((double)(this.targetScore - this.nowScore) * 0.2);
			if (Mathf.Abs((float)num2) <= 1f)
			{
				this.nowScore = this.targetScore;
			}
			else
			{
				this.nowScore += num2;
			}
			this.labNow.text = this.nowScore.ToString("N0");
			//this.txtBestScore.text = this.nowScore.ToString("N0");
			if(this.nowScore >= GameUser.Instance.bestRecord)
            {
				this.txtBestScore.text = this.nowScore.ToString();

			}
		}
	}

	public Color pinkActiveColor;

	public Color yellowActiveColor;

	public Color deactiveColor;

	public Text labTipCount;

	public Image tipStatus0;

	public Image tipStatus1;

	public Image tipImg;

	public GameObject lightGo;

	public GameObject removeGo;

	public GameObject undoGo;

	public Image undoImg;

    public Sprite undoEnable;

    public Sprite undoDisable;

	public GameObject cancelGo;

	public RectTransform guideHand;

	public RectTransform topRect;

	public RectTransform bottomRect;

	public GameObject gameGo;

	public GameObject costTimeGo;

	public GameObject iapPannel;

	public Text labNow;

	public Text txtBestScore;

	public Text labCostTim;

	public Text txtGuide;

	internal float costTim;

	internal long nowScore;

	internal long targetScore;

	internal float emptyTouchTime;

	private bool isVideoCache;

	private float videoCheckTime;
}
