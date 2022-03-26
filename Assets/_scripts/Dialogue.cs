using System.Collections;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] string _path;
    private IDialogLoader _loader;

    private void Awake()
    {
        _loader = GetComponent<IDialogLoader>();
    }
    private void PushDialogue(DialogueData dialogue)
    {
        if (dialogue == null) return;
        
        DialogSystem.Instance.EnqueueDialogue(dialogue.DialogueElements);
    }

    public void LoadDialogue()
    {
        StartCoroutine(_loader.Load(_path, PushDialogue));
    }
}