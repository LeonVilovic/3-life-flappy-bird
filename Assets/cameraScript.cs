using UnityEngine;

public class cameraScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // makes camera consistent accros all devices
        Camera.main.aspect = 16f / 6f;
    }

    // Update is called once per frame
    void Update()
    {       
    }
}
