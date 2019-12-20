using UnityEngine;

[AddComponentMenu("CoreGame/ExitLevel", 2) ,RequireComponent(typeof(Collider))]
public class LevelExit : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] sceneIndex sceneIndex;
    [SerializeField] sceneIndex currentScene;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Game.LoadScene(sceneIndex);

            if (currentScene == sceneIndex.Lvl1 && sceneIndex == sceneIndex.Lvl2)
                GameProgressTracker.NotifyCompletedLevel(currentScene);
            if (currentScene == sceneIndex.Lvl2 && sceneIndex == sceneIndex.Lvl3)
                GameProgressTracker.NotifyCompletedLevel(sceneIndex.Lvl2);
            if (currentScene == sceneIndex.Lvl3 && sceneIndex == sceneIndex.Victory)
                GameProgressTracker.NotifyCompletedLevel(sceneIndex.Lvl3);
        }
    }
}
