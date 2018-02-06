using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public PlayerController player;

    void OnTriggerEnter()
    {
        player.PlayersCurrentHealth = player.PlayersMaxHealth;
        gameObject.SetActive(false);
    }
}
