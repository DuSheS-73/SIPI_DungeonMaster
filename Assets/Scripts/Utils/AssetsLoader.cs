using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class AssetsLoader
{
    private GameObject _cachedObject;

    public async Task<T> LoadAsync<T>(string assetId)
    {
        var handle = Addressables.InstantiateAsync(assetId);
        _cachedObject = await handle.Task;

        if (!_cachedObject.TryGetComponent(out T component))
            throw new NullReferenceException($"Object of type {typeof(T)} is NULL on attempt to load it from addressables");

        return component;
    }

    public void Unload()
    {
        if (_cachedObject == null)
            return;

        _cachedObject.SetActive(false);
        Addressables.ReleaseInstance(_cachedObject);
        _cachedObject = null;
    }

    public void UnloadUncached(GameObject uncachedObject)
    {
        if (uncachedObject == null)
            return;

        uncachedObject.SetActive(false);
        Addressables.ReleaseInstance(uncachedObject);
    }
}
