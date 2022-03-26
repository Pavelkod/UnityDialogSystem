using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueElement
{
    public string Title;
    [TextArea(4, 15)]
    public string Body;
    public string TextColorHex;
    public string BackgroundUrl;
    public string TitleTextColorHex;
    public string TitleBackGroundUrl;
    public DialogueType Type;
    public List<ButtonData> Buttons;
}
