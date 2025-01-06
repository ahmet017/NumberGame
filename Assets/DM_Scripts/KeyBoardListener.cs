/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
using UnityEngine;

public class KeyBoardListener : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && this.onBackKeyEvent != null)
		{
			this.onBackKeyEvent();
		}
	}

	public KeyBoardListener.OnBackKeyEvent onBackKeyEvent;

	public delegate void OnBackKeyEvent();
}
