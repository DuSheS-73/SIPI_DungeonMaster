using UnityEngine;

public class GeneralPlayerData : MonoBehaviour
{
    [SerializeField] private TitleScreenUI _ui;

    [SerializeField] private int _coins = 500;

    public int Coins => _coins;

    private void Start()
    {
        var saveSystem = new SaveSystem();
        PlayerSaveData saveData = saveSystem.LoadGeneralPlayerData();

        if (saveData.Coins >= 0)
            _coins = saveData.Coins;

        _ui.SetCoins(_coins);
    }

    /// <summary>
    /// Adds coins to player
    /// </summary>
    /// <param name="amount">Amount of coins to add</param>
    public void AddCoins(int amount)
    {
        _coins += amount;

        _ui.SetCoins(_coins);

        var saveSystem = new SaveSystem();
        saveSystem.SaveGeneralPlayerData(this);
    }
}
