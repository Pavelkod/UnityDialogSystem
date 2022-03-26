using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesTexturesLoader : MonoBehaviour, ISpritePorvider
{
    [SerializeField] string _baseDir;

    public bool IsLoaded { get; private set; }
    private List<Texture2D> _texs;

    public event Action OnLoadingDone;
    private void Awake()
    {
        _texs = new List<Texture2D>(Resources.LoadAll<Texture2D>("UI"));
        IsLoaded = true;
    }

    public void Add(string url)
    {

    }

    public Sprite Get(string url)
    {
        return _texs.Where(x => x.name == url).Select(x =>
        {
            return Sprite.Create(x, new Rect(0, 0, x.width, x.height), Vector2.one / 2);
        }).FirstOrDefault();
    }
}
