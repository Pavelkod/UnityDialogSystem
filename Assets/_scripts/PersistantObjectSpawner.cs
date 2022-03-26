using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject _persistantObjectsPrefab;
    private static bool _isSpawned = false;
    private void Awake()
    {
        if (_isSpawned) return;
        _isSpawned = true;
        var persistantObject = Instantiate(_persistantObjectsPrefab);
        DontDestroyOnLoad(persistantObject);

    }
}
