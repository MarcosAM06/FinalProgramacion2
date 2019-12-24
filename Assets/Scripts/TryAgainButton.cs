using UnityEngine;

public class TryAgainButton : MonoBehaviour
{
    LevelManager level;

    private void Awake()
    {
        level = FindObjectOfType<LevelManager>();
    }

    public void LoadLastLevel()
    {
        var save = level.LoadGame();

        if (!save.playerSavedGame)
            Game.LoadScene(SceneIndex.Lvl1); //Morimos en el primer nivel asi que no se guardo nada.
        else
        {
            if (save.lastScenePlayed == SceneIndex.Lvl1)
                Game.LoadScene(SceneIndex.Lvl2);
            if (save.lastScenePlayed == SceneIndex.Lvl2 || 
                save.lastScenePlayed == SceneIndex.Lvl3)
                Game.LoadScene(SceneIndex.Lvl3);
        }
    }
}
