using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] private AssetReference _pauseMenu;

    private GameObject _cachedObject;

    /// <summary>
    /// Sets the timeScale to 0 to stop the game
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0f;

        //_pauseMenu.InstantiateAsync();
    }

    /// <summary>
    /// Sets the timeScale to 1 to resume the game
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1f;
    }
}
