using UnityEngine;

public class LoadTagScene : MonoBehaviour
{
    public GameObject foxModel;
    public GameObject foxTwoModel;

    public float collisionDisappearTime = 3f; // Disappears 3 sec after collision
    public float maxSurvivalTime = 30f; // Max time before disappearing

    void Start()
    {
    foxModel.SetActive(false);
    }

    public void ActivateFox()
    {
        if (foxModel != null)
        {
            foxModel.SetActive(true);
            Debug.Log("Fox will be gone in " + maxSurvivalTime + " seconds.");
            Invoke("DeactivateFox", maxSurvivalTime);
            foxTwoModel.SetActive(false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Fox touched you!!");
            CancelInvoke("DeactivateFox");
            Invoke("DeactivateFox", collisionDisappearTime); //bye fox
        }
    }

    void DeactivateFox()
    {
        if (foxModel != null)
        {
            foxModel.SetActive(false);
            foxTwoModel.SetActive(true);

            Debug.Log("Fox has disappeared.");
        }
    }
}
