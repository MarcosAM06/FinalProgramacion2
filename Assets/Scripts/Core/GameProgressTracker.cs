using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Core.Serialization;

public class GameProgressTracker : MonoBehaviour
{
    public static GameProgressTracker instance;
    public const string DataPath = "Assets/Data/Archievements.dat";

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

    public static ArchivementData GetUnlockedArchivements()
    {
        var LoadedData = FullSerialization.Deserialize<ArchivementData>(DataPath, false);
        if (LoadedData == null) return new ArchivementData();
        return LoadedData;
    }

    public static void NotifyCompletedLevel(SceneIndex level)
    {
        ArchivementData unlockedArchivements = GetUnlockedArchivements();

        switch (level)
        {
            case SceneIndex.Lvl1:
                unlockedArchivements.Nivel1Completado = true;
                Analytics.CustomEvent("Level1Finalized");
                break;
            case SceneIndex.Lvl2:
                unlockedArchivements.Nivel2Completado = true;
                Analytics.CustomEvent("Level2Finalized");
                break;
            case SceneIndex.Lvl3:
                unlockedArchivements.JuegoCompletado = true;
                Analytics.CustomEvent("GameCompleted");
                break;
            default:
                break;
        }

        unlockedArchivements.Serialize(DataPath, false);
    }

    public static void NofitySimpleEnemyKilled()
    {
        ArchivementData unlockedArchivements = GetUnlockedArchivements();

        unlockedArchivements.TotalEnemigosSimplesAsesinados++;

        if (unlockedArchivements.TotalEnemigosSimplesAsesinados == 5)
            unlockedArchivements.EnemigoSimplesAsesinados = true;

        //LLamamos a los eventos de Analitics.
        AnalyticsEvent.Custom("SimpleEnemyiKilled", new Dictionary<string, object>
        {
            { "Total killed", unlockedArchivements.EnemigoSimplesAsesinados }
        });

        unlockedArchivements.Serialize(DataPath, false);
    }

    public static void NotifyRangeEnemyKilled()
    {
        ArchivementData unlockedArchivements = GetUnlockedArchivements();

        unlockedArchivements.TotalEnemigosRangoAsesinados++;

        if (unlockedArchivements.TotalEnemigosRangoAsesinados == 5)
            unlockedArchivements.EnemigosRangoAsesinados = true;

        //LLamamos a Analitics again.
        AnalyticsEvent.Custom("RangeEnemyKilled", new Dictionary<string, object>
        {
            { "Total killed", unlockedArchivements.EnemigosRangoAsesinados }
        });

        unlockedArchivements.Serialize(DataPath, false);
    }

    public static void NotifyPlayerDied()
    {
        ArchivementData unlockedArchivements = GetUnlockedArchivements();

        if (!unlockedArchivements.PrimeraMuerte)
        {
            unlockedArchivements.PrimeraMuerte = true;
            
            //Notificamos a Analitics.
            Analytics.CustomEvent("PlayerFirstDeath");
        }

        unlockedArchivements.Serialize(DataPath, false);
    }

    public static void BossEnemyKilled()
    {
        ArchivementData unlockedArchivements = GetUnlockedArchivements();

        if (!unlockedArchivements.BossAsesinado)
        {
            unlockedArchivements.BossAsesinado = true;
            //Notificación.
            Analytics.CustomEvent("BossKilled");
        }

        unlockedArchivements.TotalBossesEliminados++;
        unlockedArchivements.Serialize(DataPath, false);
    }
}
