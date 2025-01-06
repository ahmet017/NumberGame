
using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class Localization
{
    private Localization()
    {
    }

    private static bool IosJugeLanguage(string countrycode, string head)
    {
        return countrycode == head || countrycode.Contains(head + "-");
    }

    private static bool AndroidJugeLanguage(string countrycode, string head)
    {
        return countrycode == head || countrycode.Contains(head + "_");
    }

    public static void Init()
    {
        int num = -1;
        if (num == -1)
        {
            
        }
        Localization.instance.curIndex = num;
        Localization.instance.langs = new List<string>();
        Localization.instance.langData = new Dictionary<string, List<string>>();
        List<List<string>> list = CSVReader.ReadCSVData("binaryData/localization", 0);
        for (int i = 1; i < list[0].Count; i++)
        {
            Localization.instance.langs.Add(list[0][i]);
        }
        for (int j = 1; j < list.Count; j++)
        {
            List<string> list2 = new List<string>();
            for (int k = 1; k < list[j].Count; k++)
            {
                list2.Add(list[j][k]);
            }
            Localization.instance.langData.Add(list[j][0], list2);
        }
    }

    public static Localization getInstance()
    {
        return Localization.instance;
    }

    public static bool IsChina()
    {
        return Localization.instance.curIndex == 8;
    }

    //public static string getCurFlag()
    //{
    //    //return Localization.instance.langs[Localization.instance.curIndex];
    //}

    public static bool switchLang(int index)
    {
        if (index < Localization.instance.langs.Count && index >= 0)
        {
            Localization.instance.curIndex = index;
            Storage.WriteConfig("localization", index.ToString());
            return true;
        }
        return false;
    }

    public static bool switchLang(string langStr)
    {
        int index = -1;
        for (int i = 0; i < Localization.instance.langs.Count; i++)
        {
            if (Localization.instance.langs[i] == langStr)
            {
                index = i;
                break;
            }
        }
        return Localization.switchLang(index);
    }

    public static string getLocalString(string key, string defaultText = "")
    {
        //if (Localization.instance.langData.ContainsKey(key))
        //{
        //	return Localization.instance.langData[key][Localization.instance.curIndex];
        //}
        return defaultText;
    }

    private static readonly Localization instance = new Localization();

    public int curIndex;

    private Dictionary<string, List<string>> langData;

    public List<string> langs;
}
