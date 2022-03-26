using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueButton : MonoBehaviour, IEquatable<ButtonData>
{
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Image _image;
    [SerializeField] private Animator _animator;
    private ButtonData _buttonData;
    private int _showHash = Animator.StringToHash("show");
    private ButtonAction _actionId;
    private Action<ButtonAction> _onButtonClickCallback;
    public void Init(ButtonData buttonData, Action<ButtonAction> onButtonClick, ISpritePorvider spritePorvider)
    {
        _actionId = buttonData.ActionId;
        _buttonText.SetText(buttonData.ButtonName);
        if (!String.IsNullOrEmpty(buttonData.BackgroundUrl))
        {
            _image.sprite = spritePorvider.Get(buttonData.BackgroundUrl);
            _image.enabled = true;
        }
        _buttonText.color = buttonData.buttonTextColorHex.HexStringToColor();
        _onButtonClickCallback = onButtonClick;
    }

    public void Show() => _animator.SetBool(_showHash, true);

    public void OnButtonClick() => _onButtonClickCallback?.Invoke(_actionId);

    public bool Equals(ButtonData other)
    {
        return _buttonData.BackgroundUrl == other.BackgroundUrl && _buttonData.ButtonName == other.ButtonName && _buttonData.ActionId == other.ActionId;
    }
}
