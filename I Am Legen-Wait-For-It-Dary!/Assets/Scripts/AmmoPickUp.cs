using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public Weapon weapon;

    void OnTriggerEnter()
    {
        weapon.AmmoLeft += 30; 
        gameObject.SetActive(false);
    }
}
