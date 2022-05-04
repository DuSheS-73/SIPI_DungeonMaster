using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    [Header("Buttons")]
    [SerializeField] private Button _upgradeButton;

    public void ReplaceTextVariables(string statName, int price)
    {
        _text.SetText($"Upgrade {statName} for {price}?");
    }

    public void SetUpgaradeButtonInteractable(bool interactable)
    {
        _upgradeButton.interactable = interactable;
    }
}
