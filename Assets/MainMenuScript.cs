using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public float inputDelay = 2f; // Seconds to wait before input works
    private bool canInput = false;

    void Start()
    {
        // Start a coroutine to enable input after delay
        StartCoroutine(EnableInputAfterDelay());
    }

    void Update()
    {
        if (!canInput) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            SceneManager.LoadScene("Game");
        }
    }

    IEnumerator EnableInputAfterDelay()
    {
        yield return new WaitForSeconds(inputDelay);
        canInput = true;
    }
}
