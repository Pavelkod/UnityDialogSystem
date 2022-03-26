using System;
using UnityEngine;

public interface ISpritePorvider
{
    public bool IsLoaded { get; }
    public event Action OnLoadingDone;
    public void Add(string url);
    Sprite Get(string url);
}
