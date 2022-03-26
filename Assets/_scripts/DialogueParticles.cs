using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private int _pixelsPerPoint = 100;

    private void OnEnable()
    {
        DialogSystem.OnDialogueChangeLayout += Show;
        DialogSystem.OnDialogueClose += Hide;
    }

    private void OnDisable()
    {
        DialogSystem.OnDialogueChangeLayout -= Show;
        DialogSystem.OnDialogueClose -= Hide;
    }
    private void Show(RectTransform rect)
    {
        transform.position = Utils.GetUIWorldPos(rect);
        var size = rect.rect.size;
        var scale = new Vector3(size.x, size.y, _pixelsPerPoint) / _pixelsPerPoint;
        var shape = _particles.shape;
        shape.scale = scale;
        _particles.Play();
    }

    private void Hide()
    {
        _particles.Stop();
    }
}
