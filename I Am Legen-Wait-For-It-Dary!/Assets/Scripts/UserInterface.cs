using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public RectTransform HealthBarFill;
    public Text AmmoText;

    public PlayerController player;
    public Weapon weapon;

    void Update()
    {
        SetHealthAmount(player.GetHealthScaled());
        SetAmmoAmount(weapon.CurrentAmmo, weapon.AmmoLeft);
    }

    void SetHealthAmount(float health)
    {
        HealthBarFill.localScale = new Vector3(health, 1f, 1f);
    }

    void SetAmmoAmount(float currentAmmo, float leftAmmo)
    {
        AmmoText.text = currentAmmo.ToString() + " / " + leftAmmo.ToString();
    }
}
