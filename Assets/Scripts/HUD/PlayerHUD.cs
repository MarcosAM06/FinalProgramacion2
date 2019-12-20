using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TMP_Text _healthText = null;
    public TMP_Text _AmmoText = null;
    public TMP_Text _magazineText = null;

    [SerializeField] Joystick joystick = null;
    [SerializeField] Button _switchWeaponLeft = null;
    [SerializeField] Button _switchWeaponRight = null;
    [SerializeField] Button _shootButton = null;

    public void HideControls()
    {
        joystick.gameObject.SetActive(false);
        _healthText.gameObject.SetActive(false);
        _AmmoText.gameObject.SetActive(false);
        _magazineText.gameObject.SetActive(false);
        _switchWeaponLeft.gameObject.SetActive(false);
        _switchWeaponRight.gameObject.SetActive(false);
        _shootButton.gameObject.SetActive(false);
    }

    public void SetHealthDisplay(int Health, string preMessage = "HEALTH")
    {
        _healthText.text = string.Format("{0}: {1}", preMessage, Health);
    }
}
