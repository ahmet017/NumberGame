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

public class LangController : MonoBehaviour
{
    [SerializeField] private Dropdown dropdownLang;

	private void Start()
	{

		dropdownLang.onValueChanged.AddListener(delegate
		{
			DropdownValueChanged(dropdownLang);
		});

		// Load saved value
		int savedValue = PlayerPrefs.GetInt("SelectedDropdownValue", 0); // default value is 0
		dropdownLang.value = savedValue;

	}


	void DropdownValueChanged(Dropdown change)
	{
		// Save selected value
		PlayerPrefs.SetInt("SelectedDropdownValue", change.value);
		PlayerPrefs.Save();

		// You can perform other actions based on the selected value if needed
		Debug.Log("Selected Dropdown Value: " + change.value);
	}

	public void GetDropDowValue()
	{
		//int indexDropDow = dropdownLang.value;

		//Debug.Log("DROPDOW LANG :" + dropdownLang.value);

		switch (dropdownLang.value)
		{
			case 0:
				Gley.Localization.API.SetCurrentLanguage(SupportedLanguages.English);
				//RefreshText();
				
				break;
			case 1:
				Gley.Localization.API.SetCurrentLanguage(SupportedLanguages.French);
				//RefreshText();
				break;

			case 2:
				Gley.Localization.API.SetCurrentLanguage(SupportedLanguages.Spanish);
				//RefreshText();
				break;
			case 3:
				Gley.Localization.API.SetCurrentLanguage(SupportedLanguages.Turkish);
				//RefreshText();
				break;
			case 4:
				Gley.Localization.API.SetCurrentLanguage(SupportedLanguages.ChineseSimplified);
				//RefreshText();
				break;
			case 5:
				Gley.Localization.API.SetCurrentLanguage(SupportedLanguages.Korean);
				//RefreshText();
				break;
			case 6:
				Gley.Localization.API.SetCurrentLanguage(SupportedLanguages.Russian);
				//RefreshText();
				break;

		}
	}
}
