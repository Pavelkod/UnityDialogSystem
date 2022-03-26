using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class Utils
{
    private static Camera _camera;
    public static Camera Camera
    {
        get
        {
            if (_camera == null) _camera = Camera.main;
            return _camera;
        }
    }

    public static Vector2 GetUIWorldPos(RectTransform rectTransform)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, rectTransform.position, Camera, out Vector3 point);
        return point;
    }

    public static Color HexStringToColor(this string hex)
    {
        MatchCollection matches = Regex.Matches(hex, "(?i)#?([0-9a-f]{2})");
        if (matches.Count < 3) return Color.white;
        return new Color32(HexToInt(matches[0].Groups[1].Value), HexToInt(matches[1].Groups[1].Value), HexToInt(matches[2].Groups[1].Value), 255);
    }

    private static byte HexToInt(string hex)
    {
        return Convert.ToByte(hex, 16);
    }
}
