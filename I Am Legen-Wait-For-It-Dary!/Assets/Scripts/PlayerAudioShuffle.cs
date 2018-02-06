using UnityEngine;

public class PlayerAudioShuffle : MonoBehaviour
{
    public AudioSource AudioSource;
    private bool wasPlayed = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !wasPlayed)
        {
            AudioSource.Play();
            wasPlayed = true;
        }
    }
    

}
