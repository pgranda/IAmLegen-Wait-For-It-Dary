using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    public Light light;

    void Update()
    {
        if (Random.value > 0.9)
        {
            if (light.enabled == true)
            {
                light.enabled = false;
            }
            else
            {
                light.enabled = true;
            }
        }
    }

}
