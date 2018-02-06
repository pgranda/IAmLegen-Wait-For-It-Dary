using UnityEngine;

public class GameObjectiveSpotlight : MonoBehaviour
{
    public GameObject ObjectiveText;
    public Light SpotLight;
    
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (SpotLight.range >= 0 && SpotLight.range < 20)
            {
                SpotLight.range++;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            ObjectiveText.gameObject.SetActive(false);
        }
     
    }
}
