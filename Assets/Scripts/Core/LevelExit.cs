using UnityEngine;

[AddComponentMenu("CoreGame/ExitLevel", 2) ,RequireComponent(typeof(Collider))]
public class LevelExit : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] sceneIndex sceneIndex;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Game.LoadScene(sceneIndex);
    }
}
