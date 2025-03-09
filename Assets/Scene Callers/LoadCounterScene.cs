using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCounterScene : MonoBehaviour
{
    public void LoadFabianScene()
    {
        Debug.Log("Button pressed! Attempting to load DUCKY ONLY...");

        Scene scene = SceneManager.GetSceneByName("Fabian");

        if (scene.isLoaded)
        {
            Debug.Log("DUCKY ONLY scene is already loaded.");
            return;
        }

        SceneManager.LoadScene("Fabian", LoadSceneMode.Additive);
        Debug.Log("Fabian scene is now loading...");
    }
}
