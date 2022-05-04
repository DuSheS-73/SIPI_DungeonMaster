using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Provides logic for character selection in the main menu
/// </summary>
public class CharacterSelector : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _nextButton;

    [Header("UI")]
    [SerializeField] private TitleScreenUI _ui;
    [SerializeField] private GameObject _comingSoonBanner;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _characterStatsContainer;

    [Header("Characters")]
    [SerializeField] private List<Character> _characters;

    private CameraController _camera;

    private int _selectedCharacterIndex = 0;

    public Character SelectedCharacter => _characters[_selectedCharacterIndex];

    private void Start()
    {
        _camera = Camera.main.GetComponent<CameraController>();

        _prevButton.onClick.AddListener(delegate { SelectNextCharacter(-1); });
        _nextButton.onClick.AddListener(delegate { SelectNextCharacter(1); });

        _startButton.onClick.AddListener(StartTheGame);

        var saveSystem = new SaveSystem();
        for (int i = 0; i < _characters.Count; i++)
        {
            saveSystem.TryLoadCharacter(_characters[i]);
        }

        _ui.SetCharacterStats(_characters[0]);
    }


    /// <summary>
    /// Changes the selected character and moves camera to the next room 
    /// </summary>
    /// <param name="n"></param>
    private void SelectNextCharacter(int n)
    {
        _selectedCharacterIndex += n;

        _camera.MoveTo(new Vector3(_selectedCharacterIndex * 8, _camera.transform.position.y, _camera.transform.position.z));

        bool isLast = _selectedCharacterIndex == _characters.Count;

        _nextButton.gameObject.SetActive(!isLast);

        _comingSoonBanner.SetActive(isLast);
        _characterStatsContainer.SetActive(!isLast);

        _startButton.interactable = !isLast;

        if (!isLast)
        {
            _ui.SetCharacterStats(_characters[_selectedCharacterIndex]);
        }


        _prevButton.gameObject.SetActive(_selectedCharacterIndex != 0);
    }


    /// <summary>
    /// Start button click event.
    /// Changes main menu scene to game scene
    /// </summary>
    private void StartTheGame()
    {
        PlayerPrefs.SetInt("selectedCharacterIndex", _selectedCharacterIndex);

        _loadingScreen.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
