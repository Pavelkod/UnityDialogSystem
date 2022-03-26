using System;
using UnityEngine;

public interface ISpriteLoader
{
    public bool IsLoaded { get; }
    public event Action OnLoadingDone;
    public void Add(string url);

    public Sprite Get(string url);
}