using UnityEngine;
using UnityEditor;

[AddComponentMenu("CoreGame/ExitLevel", 2) ,RequireComponent(typeof(Collider))]
public class LevelExit : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] SceneIndex sceneIndex;
    [SerializeField] bool saveAtExit = true;

    LevelManager level;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
        level = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            level.SaveGame();
            AssetDatabase.SaveAssets();

            if (level.CurrentScene == SceneIndex.Lvl1 && sceneIndex == SceneIndex.Lvl2)
                GameProgressTracker.NotifyCompletedLevel(level.CurrentScene);
            if (level.CurrentScene == SceneIndex.Lvl2 && sceneIndex == SceneIndex.Lvl3)
                GameProgressTracker.NotifyCompletedLevel(SceneIndex.Lvl2);
            if (level.CurrentScene == SceneIndex.Lvl3 && sceneIndex == SceneIndex.Victory)
                GameProgressTracker.NotifyCompletedLevel(SceneIndex.Lvl3);

            Game.LoadScene(sceneIndex);
        }
    }
}
