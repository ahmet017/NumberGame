
using System;
using UnityEngine;

public class VectorNativeInstance : MonoBehaviour
{
	private void Awake()
	{
		if (!VectorNativeInstance.alreadyCreate)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
		VectorNativeInstance.alreadyCreate = true;
	}

	private void Update()
	{
	}



	public VectorNativeInstance.InvokeNativeCallback mCallBack;

	private static bool alreadyCreate;

	public delegate void InvokeNativeCallback(int type, string msg);
}
