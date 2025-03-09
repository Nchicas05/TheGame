using UnityEngine;

public class LoadTagScene : MonoBehaviour
{
    public GameObject foxModel; 
    public float disappearTime = 3f; 

    void Start()
    {
        if (foxModel != null)
        {
            foxModel.SetActive(false); 
        }
        else
        {
            Debug.LogError("Fox model is not assigned in the Inspector!");
        }
    }

    public void ActivateFox()
    { 
    foxModel.SetActive(true); 
    Debug.Log("Fox is now active!"); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Fox touched you!!");
            Invoke("DeactivateFox", disappearTime);
        }
    }

    void DeactivateFox()
    {
        if (foxModel != null)
        {
            foxModel.SetActive(false);
            Debug.Log("Fox has disappeared.");
        }
    }
}
