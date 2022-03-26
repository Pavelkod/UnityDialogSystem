using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AssetsDialogLoader : MonoBehaviour, IDialogLoader
{
    public IEnumerator Load(string url, Action<DialogueData> onComplete)
    {
        StreamReader stream = new StreamReader(url);
        var json = stream.ReadToEnd();
        stream.Close();
        DialogueData obj = JsonUtility.FromJson<DialogueData>(json);
        onComplete.Invoke(obj);
        yield break;
    }

}
