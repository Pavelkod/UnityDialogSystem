using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWindowHandler : MonoBehaviour
{
    [SerializeField] private DialogueButton _buttonPrefab;
    [SerializeField] private Transform _buttonParrent;
    [SerializeField] private TextMeshProUGUI _bodyText;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private float _textAnimationSpeed = 0;
    [SerializeField] private float _buttonSequenceTimeout = .1f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Action<ButtonAction> _onDialogButtonClick;
    [SerializeField] private Image _background;
    [SerializeField] private Image _titleBackground;
    [SerializeField] public DialogueType Type;
    [SerializeField] private ISpritePorvider _spriteProvider;
    private List<DialogueButton> _buttons = new List<DialogueButton>();
    private int _showHash = Animator.StringToHash("show");

    public void Init(Action<ButtonAction> onButtonClickCallback, ISpritePorvider spritePorvider)
    {
        _onDialogButtonClick = onButtonClickCallback;
        _spriteProvider = spritePorvider;
    }
    private async void MakeButtons(List<ButtonData> buttonsData)
    {
        foreach (var button in _buttons)
            Destroy(button.gameObject);
        _buttons.Clear();

        foreach (var buttonData in buttonsData)
        {
            var button = Instantiate(_buttonPrefab, _buttonParrent);
            button.Init(buttonData, _onDialogButtonClick, _spriteProvider);
            _buttons.Add(button);
        }

        foreach (var button in _buttons)
        {
            button.Show();
            await Task.Delay((int)(_buttonSequenceTimeout * 1000));
        }

    }

    public void Show(DialogueElement dialogueElement)
    {
        StopAllCoroutines();
        _animator.SetBool(_showHash, true);
        _titleText.SetText(dialogueElement.Title);
        _titleText.color = dialogueElement.TitleTextColorHex.HexStringToColor();
        _bodyText.color = dialogueElement.TextColorHex.HexStringToColor();
        _background.sprite = _spriteProvider.Get(dialogueElement.BackgroundUrl);
        _titleBackground.sprite = _spriteProvider.Get(dialogueElement.TitleBackGroundUrl);
        StartCoroutine(ShowBodyText(dialogueElement.Body));

        MakeButtons(dialogueElement.Buttons);
    }

    public void Hide()
    {
        StopAllCoroutines();
        _animator.SetBool(_showHash, false);
    }

    IEnumerator ShowBodyText(string body)
    {
        string animatedStr = "";
        foreach (var letter in body.ToCharArray())
        {
            animatedStr += letter;
            _bodyText.SetText(animatedStr);
            yield return new WaitForSeconds(_textAnimationSpeed);
        }
    }
}
