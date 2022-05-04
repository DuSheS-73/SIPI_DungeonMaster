public class PlayerSaveData
{
    public PlayerSaveData()
    {
        Coins = -1;
    }

    public PlayerSaveData(int coins)
    {
        Coins = coins;
    }

    public int Coins { get; }
}
