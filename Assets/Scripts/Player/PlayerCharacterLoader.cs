using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterLoader : LoadingAffector
{
    [Header("Characters")]
    [SerializeField] private List<Character> _characters;

    private void Start()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("selectedCharacterIndex");
        Character character = _characters[selectedCharacterIndex];

        var saveSystem = new SaveSystem();
        saveSystem.TryLoadCharacter(character);

        Player.Instance.Init(character);

        Ready = true;
    }
}
