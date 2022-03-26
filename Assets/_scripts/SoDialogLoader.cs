using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoDialogLoader : MonoBehaviour, IDialogLoader
{
    [SerializeField] private DialogueScriptableObject _dialogue;
    public IEnumerator Load(string url, Action<DialogueData> onComplete)
    {
        var dialogData = new DialogueData();
        dialogData.DialogueElements = _dialogue.DialogueElements;
        onComplete(dialogData);
        yield break;
    }
}
