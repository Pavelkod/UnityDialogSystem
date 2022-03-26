using System;
using System.Collections;

public interface IDialogLoader
{
    IEnumerator Load(string url, Action<DialogueData> onComplete);
}
