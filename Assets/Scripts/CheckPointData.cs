using System;
using UnityEngine;

[Serializable]
public class CheckPointData
{
    public bool playerSavedGame = false;
    public SceneIndex lastScenePlayed = SceneIndex.Lvl1;
    public int healthAmmount;

    [Header("Rifle")]
    public int BulletsInMagazine;
    public int BulletsInBackPack;
    public bool hasInfiniteBullets;
}
