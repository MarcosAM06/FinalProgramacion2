using UnityEngine;
using Core.Serialization;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public SceneIndex CurrentScene;
    public string saveGamePath = "SaveGamePath";
    public bool isPlayableScene = true;

    Player player;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);

        if (isPlayableScene)
        {
            CheckPointData lastData = LoadGame();

            if (lastData != null && lastData.playerSavedGame)
            {
                player = FindObjectOfType<Player>();
                player.GetWeaponByType(WeaponType.AssaultRifle);

                if (lastData.lastScenePlayed == CurrentScene)
                {
                    player.Health = lastData.healthAmmount;

                    Weapon rifle =  player.GetWeaponByType(WeaponType.AssaultRifle);
                    rifle.LoadBulletInfo(lastData.BulletsInMagazine, lastData.BulletsInBackPack);
                }
            }
        }
    }

    public CheckPointData LoadGame()
    {
        return FullSerialization.Deserialize<CheckPointData>(saveGamePath, false);
    }

    public void SaveGame()
    {
        CheckPointData lastData = LoadGame();
        if (lastData == null)
            lastData = new CheckPointData();

        if (isPlayableScene)
        {
            player = FindObjectOfType<Player>();
            lastData.healthAmmount = player.Health;


            Weapon rifle = player.GetWeaponByType(WeaponType.AssaultRifle);
            lastData.BulletsInMagazine = rifle.Magazine;
            lastData.BulletsInBackPack = rifle.backPack;
            lastData.hasInfiniteBullets = rifle.InfiniteBullets;

            lastData.playerSavedGame = true;

            lastData.Serialize(saveGamePath, false);
        }
    }

    public void ClearGameData()
    {
        CheckPointData data = LoadGame();
        if (data == null)
            data = new CheckPointData();

        data.playerSavedGame = false;
        data.lastScenePlayed = SceneIndex.Lvl1;
        data.BulletsInMagazine = 0;
        data.BulletsInBackPack = 0;
        data.healthAmmount = 0;
        data.hasInfiniteBullets = false;

        data.Serialize(saveGamePath, false);
    }
}
