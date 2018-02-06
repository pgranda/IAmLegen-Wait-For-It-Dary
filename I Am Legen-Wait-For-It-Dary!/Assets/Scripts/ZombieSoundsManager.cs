using System.Collections;
using UnityEngine;

public class ZombieSoundsManager : MonoBehaviour
{
    public AudioSource AudioSource;
    public Enemy Enemy;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Random.value * 5 > 4.95f && AudioSource.isPlaying == false && Enemy.isDead == false)
            {
                AudioSource.Play();
            }
        }
    }


}
