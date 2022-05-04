using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _earnedCoinsText;

    public void Init(int earnedCoins)
    {
        _earnedCoinsText.SetText(earnedCoins.ToString());
    }

    /// <summary>
    /// Back to main menu button event.
    /// Loads the main menu scene
    /// </summary>
    public void OnBackToMainMenuButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
