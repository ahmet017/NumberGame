/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
using UnityEngine;

public class InitScript
{
	public static void InitAll()
	{
		if (InitScript.alreadyInited)
		{
			return;
		}
		InitScript.alreadyInited = true;
		Input.multiTouchEnabled = false;
		Application.targetFrameRate = 60;
		UIManager.UpdateRandomState(-1);
		Storage.Init("");
//		Localization.Init();
		AudioSystem.Init();
		GameUser.ReadFromLocal();
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			InitScript.isLoginGameCenter = true;
		}
		else if (Application.platform == RuntimePlatform.Android)
		{
			InitScript.isLoginGameCenter = false;
			string msg = "";
		}
	}

	public static int version = 2;

	public static float heightSize;

	public static float widthSize;

	public static bool isLoginGameCenter;

	private static bool alreadyInited;
}
