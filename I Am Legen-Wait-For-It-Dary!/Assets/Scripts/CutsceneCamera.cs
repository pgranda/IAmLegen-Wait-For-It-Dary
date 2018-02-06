using UnityEngine;
using System.Collections;

public class CutsceneCamera : MonoBehaviour
{
    public GameObject CutSceneCamera;
    public GameObject MainCamera;
    public GameObject Player;
    public Animation Animation;
    public GameObject UI;

    void Start()
    {
        Player.GetComponent<PlayerController>().ableToMove = false;
    }

    void Update()
    {
        if (Animation.isPlaying)
        {
            return;
        }
        Player.GetComponent<PlayerController>().ableToMove = true;
        CutSceneCamera.gameObject.SetActive(false);
        MainCamera.gameObject.SetActive(true);
        UI.gameObject.SetActive(true);

    }
}
