using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class WebTextureLoader : MonoBehaviour, ISpritePorvider, ISpriteLoader
{
    [SerializeField] private Sprite _spriteFallback;
    public bool IsLoaded { get; private set; } = false;
    private int _activeDownloadTasks = 0;
    public event Action OnLoadingDone;
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    public void Add(string url)
    {
        if (String.IsNullOrEmpty(url) || _sprites.ContainsKey(url))
        {
            if (_activeDownloadTasks == 0) IsLoaded = true;
            return;
        }
        IsLoaded = false;
        StartCoroutine(LoadAsync(url));
    }

    public Sprite Get(string url)
    {
        if (_sprites.TryGetValue(url, out Sprite sprite))
            return sprite;
        else return _spriteFallback;
    }
    private IEnumerator LoadAsync(string url)
    {
        _activeDownloadTasks++;

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log($"Download error. Url: {url}, error: " + request.error);
            _sprites[url] = _spriteFallback;
        }
        else
        {
            var tex = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one / 2);
            _sprites[url] = sprite;
        }

        IsLoaded = --_activeDownloadTasks == 0;
        if (IsLoaded) OnLoadingDone?.Invoke();
    }
}