using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepBetweenScenes : MonoBehaviour
{
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Draw")
        {
            audioSource.Stop();
        }
    }
}
