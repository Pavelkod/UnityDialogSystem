using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ISpritePorvider))]
public class DialogSystem : MonoBehaviour
{
    [System.Serializable]
    private struct DialogueLayout
    {
        public DialogueType Type;
        public DialogueWindowHandler dialogueWindowPrefab;
    }
    [SerializeField] private List<DialogueLayout> _dialogueLayouts;
    [SerializeField] private Transform _canvas;
    [SerializeField] private DialogueParticles _particles;

    private DialogueWindowHandler _currentDialogueWindow;
    private ISpritePorvider _loader;
    private bool _isConversationInProgress = false;
    public static DialogSystem Instance;
    private Queue<DialogueElement> _dialogueQueue = new Queue<DialogueElement>();


    #region Events
    public static event Action<ButtonAction> OnDialogueEvent;
    public static event Action OnDialogueClose;
    public static event Action OnDialogueStart;
    public static event Action<RectTransform> OnDialogueChangeLayout;

    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);
        _loader = GetComponent<ISpritePorvider>();
    }

    private void OnEnable()
    {
        _loader.OnLoadingDone += BeginConversation;
    }

    private void OnDisable()
    {
        _loader.OnLoadingDone -= BeginConversation;
    }

    public void EnqueueDialogue(List<DialogueElement> dialogueElements)
    {
        foreach (var item in dialogueElements)
        {
            EnqueueImageDownload(item);
            _dialogueQueue.Enqueue(item);
        }
        BeginConversation();
    }

    private void EnqueueImageDownload(DialogueElement item)
    {
        foreach (var but in item.Buttons)
        {
            _loader.Add(but.BackgroundUrl);
        }

        _loader.Add(item.BackgroundUrl);
    }

    private void DrawDialogue(DialogueType type)
    {
        if (_currentDialogueWindow != null && _currentDialogueWindow.Type == type) return;

        DestroyLastDialogue();


        var layout = _dialogueLayouts.Where(d => d.Type == type).FirstOrDefault();
        if (layout.dialogueWindowPrefab == null) layout = _dialogueLayouts[0];

        _currentDialogueWindow = Instantiate(layout.dialogueWindowPrefab, _canvas);
        _currentDialogueWindow.Init(OnDialogueButton, _loader);
        OnDialogueChangeLayout?.Invoke((RectTransform)_currentDialogueWindow.transform);
    }

    private void OnDialogueButton(ButtonAction actionId)
    {
        switch (actionId)
        {
            case ButtonAction.next:
                Next();
                break;
            case ButtonAction.close:
                End();
                break;
        }

        OnDialogueEvent?.Invoke(actionId);
    }

    private void BeginConversation()
    {
        if (_isConversationInProgress || !_loader.IsLoaded) return;
        _isConversationInProgress = true;
        OnDialogueStart?.Invoke();
        Next();
    }

    private void Next()
    {
        if (_dialogueQueue.Count == 0)
        {
            End();
            return;
        }
        var dialogueElement = _dialogueQueue.Dequeue();

        DrawDialogue(dialogueElement.Type);

        StartCoroutine(ShowDialogueWindow(dialogueElement));
    }

    private IEnumerator ShowDialogueWindow(DialogueElement dialogueElement)
    {
        while (!_loader.IsLoaded) yield return new WaitForSeconds(.3f);
        _currentDialogueWindow.Show(dialogueElement);
    }


    private void DestroyLastDialogue(float delay = 2f)
    {
        if (_currentDialogueWindow == null) return;

        _currentDialogueWindow.Hide();
        Destroy(_currentDialogueWindow.gameObject, delay);
        _currentDialogueWindow = null;
    }

    private void End()
    {
        _isConversationInProgress = false;
        _dialogueQueue.Clear();
        OnDialogueClose?.Invoke();
        DestroyLastDialogue();
    }
}
