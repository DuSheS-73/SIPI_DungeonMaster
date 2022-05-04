using System;
using System.IO;
using System.Threading.Tasks;

using UnityEngine;

public class SaveSystem
{
    public void SaveGeneralPlayerData(GeneralPlayerData generalPlayerData)
    {
        string filePath = $"{Application.persistentDataPath}/genaral.master";

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            using (var binaryWriter = new BinaryWriter(fileStream))
            {
                binaryWriter.Write(generalPlayerData.Coins);
            }
        }  
    }

    public PlayerSaveData LoadGeneralPlayerData()
    {
        string filePath = $"{Application.persistentDataPath}/genaral.master";

        if (File.Exists(filePath))
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    int coins = binaryReader.ReadInt32();

                    return new PlayerSaveData(coins);
                }
            }
        }

        return new PlayerSaveData();
    }

    public void SaveCharacter(Character character)
    {
        string filePath = $"{Application.persistentDataPath}/{character.SaveFileName}.master";

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            using (var binaryWriter = new BinaryWriter(fileStream))
            {
                binaryWriter.Write(character.HealthUpgradeLevel);
                binaryWriter.Write(character.DamageUpgradeLevel);
                binaryWriter.Write(character.AttackSpeedUpgradeLevel);
                binaryWriter.Write(character.AttackRangeUpgradeLevel);
            }
        }  
    }

    public void TryLoadCharacter(Character character)
    {
        string filePath = $"{Application.persistentDataPath}/{character.SaveFileName}.master";

        if (File.Exists(filePath))
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    character.SetHealthUpgradeLevel(binaryReader.ReadInt32());
                    character.SetDamageUpgradeLevel(binaryReader.ReadInt32());
                    character.SetAttackSpeedUpgradeLevel(binaryReader.ReadInt32());
                    character.SetAttackRangeUpgradeLevel(binaryReader.ReadInt32());
                }
            }
        }   
    }
}
