using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public float inputDelay = 2f; // Seconds to wait before input works
    private bool canInput = false;
    public string SceneName;

    private AsyncOperation operation;


    void Start()
    {
        // Start a coroutine to enable input after delay
        StartCoroutine(EnableInputAfterDelay());
        StartCoroutine(LoadSceneAsync(SceneName));
    }

    void Update()
    {
        if (!canInput) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            //SceneManager.LoadScene("Game");
            operation.allowSceneActivation = true;
        }
    }

    IEnumerator EnableInputAfterDelay()
    {
        yield return new WaitForSeconds(inputDelay);
        canInput = true;
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log("Load Progress for scene: " + progress);

            yield return null;
        }
        yield return null;
    }
}
