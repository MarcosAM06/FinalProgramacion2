using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Joystick joystick = null;
    [SerializeField] TMP_Text _healthText = null;
    [SerializeField] TMP_Text _AmmoText = null;
    [SerializeField] TMP_Text _magazineText = null;
    [SerializeField] Button _switchWeaponLeft = null;
    [SerializeField] Button _switchWeaponRight = null;
    [SerializeField] Button _useHealthPackButton = null;
    [SerializeField] Button _shootButton = null;
    [SerializeField] Button _InteractButton = null;

    public void HideControls()
    {
        joystick.gameObject.SetActive(false);
        _healthText.gameObject.SetActive(false);
        _AmmoText.gameObject.SetActive(false);
        _magazineText.gameObject.SetActive(false);
        _switchWeaponLeft.gameObject.SetActive(false);
        _switchWeaponRight.gameObject.SetActive(false);
        _useHealthPackButton.gameObject.SetActive(false);
        _shootButton.gameObject.SetActive(false);
        _InteractButton.gameObject.SetActive(false);
    }

    public void SetHealthDisplay(int Health, string preMessage = "HEALTH")
    {
        _healthText.text = string.Format("{0}: {1}", preMessage, Health);
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
