using System.Linq;
using System.Collections;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    [Header("GFX")]
    [SerializeField] private GameObject _loadingScreen;

    [Header("Affectors")]
    [SerializeField] private LoadingAffector[] _loadingAffectors;

    private void Start()
    {
        StartCoroutine( LoadingCoroutine() );
    }

    private IEnumerator LoadingCoroutine()
    {
        while (_loadingAffectors.Any(a => !a.Ready))
        {
            yield return null;
        }
        
        Destroy(_loadingScreen);
    }
}
