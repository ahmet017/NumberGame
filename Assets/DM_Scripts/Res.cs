/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
using UnityEngine;

public sealed class Res
{
    public const string GAME_LINE = "prefabs/line";

    public const string GAME_DOT = "prefabs/dot";

    public static Color[] LevelColor = new Color[]
    {

        new Color(196/255f, 229/255f, 56/255f), //2
        new Color(0,155/255f,235/255f,255/255f),//4
    
        new Color(0,87/255f,255/255f,255/255f),//8
        new Color(217,95/255f,255/255f,255/255f),//16
        new Color(196/255f,0,122/255f,255/255f),//32
        new Color(246,133/255f,23/255f,255/255f),//64
        new Color(0,129/255f,0,255/255f),//128
       new Color(255,22/255f,49/255f,255/255f),//256
       new Color(83/255f, 20/255f, 184/255f),//512
       new Color(255/255f, 87/255f, 98/255f),//1024
        new Color(194/255f, 30/255f, 92/255f),
        new Color(99/255f, 65/255f, 78/255f),
        new Color(0.0784313753f, 0.392156869f, 0.6627451f),
        new Color(0.03529412f, 0.4509804f, 0.466666669f),
        new Color(0.423529416f, 0.215686277f, 0.145098045f),
        new Color(0.160784319f, 0.247058824f, 0.396078438f),
        new Color(0.329411775f, 0.129411772f, 0.5137255f),
        new Color(0f, 0f, 0f)
    };

    public const string DATA_LOCALIZATION = "binaryData/localization";

    public const string SNAPSHOT_FILE = "share.png";

    public const string SFX_BTN = "btn";

    public const string SFX_LEVEL_COMPLETE = "levelcomplete";

    public const string SFX_CAMERA = "camera";

    public const string SFX_SCORE = "sfx_score";

    public const string SFX_DOT_HEAD = "p";

    public const string SFX_NEW_LEVEL = "newlevel";

    public const string SFX_PAGE_FLIP = "pageflip";

    public const string SFX_PAGE_FLIP_MORE = "pageflip_more";
}
