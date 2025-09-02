using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    private static PersistentObject instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
