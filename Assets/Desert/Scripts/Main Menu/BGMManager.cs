using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the BGM object alive between scenes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }
}
