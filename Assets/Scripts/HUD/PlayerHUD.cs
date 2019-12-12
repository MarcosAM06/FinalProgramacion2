using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] TMP_Text Health = null;
    [SerializeField] Button _shootButton = null;
    [SerializeField] Button _InteractButton = null;

    public void SetHealthDisplay(int Health, string preMessage = "HEALTH")
    {
        this.Health.text = string.Format("{0}: {1}", preMessage, Health);
    }

    public void SwitchToInteractButton()
    {
        _InteractButton.gameObject.SetActive(true);
        _shootButton.gameObject.SetActive(false);
    }

    public void SwitchToShootButton()
    {
        _shootButton.gameObject.SetActive(true);
        _InteractButton.gameObject.SetActive(false);
    }
}
