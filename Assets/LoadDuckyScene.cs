using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDuckyScene : MonoBehaviour
{
    public void LoadDuckyOnlyScene()
    {
        Debug.Log("Button pressed! Attempting to load DUCKY ONLY...");

        Scene scene = SceneManager.GetSceneByName("DuckyOnly");

        if (scene.isLoaded)
        {
            Debug.Log("DUCKY ONLY scene is already loaded.");
            return;
        }

        SceneManager.LoadScene("DuckyOnly", LoadSceneMode.Additive);
        Debug.Log("DUCKY ONLY scene is now loading...");
    }
}
