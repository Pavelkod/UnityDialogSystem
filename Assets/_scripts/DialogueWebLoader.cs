using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DialogueWebLoader : MonoBehaviour, IDialogLoader
{
    private DialogueData _dialogue;
    public IEnumerator Load(string url, Action<DialogueData> onComplete)
    {
        using var request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            var json = request.downloadHandler.text;
            _dialogue = JsonUtility.FromJson<DialogueData>(json);
        }
        onComplete?.Invoke(_dialogue);
    }
}
