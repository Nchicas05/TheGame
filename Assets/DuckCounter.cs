using UnityEngine;

using TMPro;

public class DuckCounter : MonoBehaviour
{
    public TextMeshProUGUI duckCounterText; // Assign this in the Inspector
    public TextMeshProUGUI timerText; // Assign this in the Inspector
    public TextMeshProUGUI fastestTimeText; // Assign this in the Inspector
    public int maxDucks = 5; // Set the max number of ducks in the Inspector

    private int duckCount = 0;
    private float timer = 0f;
    private bool isTimerRunning = true;
    private string fastestTimeKey = "FastestTime"; // Key for PlayerPrefs

    void Start()
    {
        // Initialize UI
        if (duckCounterText != null)
        {
            UpdateUI();
        }

        // Load the fastest time from PlayerPrefs
        LoadFastestTime();
    }

    void Update()
    {
        // Update the timer if it's running
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            UpdateTimerUI();
        }

        // Simulator workaround: Detect 'G' key press
        if (Input.GetKeyDown(KeyCode.G))
        {
            // Simulate grabbing an object
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10f))
            {
                UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable = hit.collider.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
                if (interactable != null)
                {
                    OnGrabbed(interactable);
                }
            }
        }
    }

    // This method will be called when an object is grabbed (via VR controller or simulator)
    public void OnGrabbed(UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable grabbedObject)
    {
        if (grabbedObject == null)
        {
            return;
        }

        IncreaseDuckCount();
        Destroy(grabbedObject.gameObject); // Make the grabbed object disappear
    }

    public void IncreaseDuckCount()
    {
        duckCount++;
        UpdateUI();

        // Check if all ducks are found
        if (duckCount >= maxDucks)
        {
            isTimerRunning = false; // Stop the timer
            CheckForFastestTime(); // Check and save the fastest time
        }
    }

    private void UpdateUI()
    {
        if (duckCounterText != null)
        {
            if (duckCount >= maxDucks)
            {
                duckCounterText.text = "ALL FOUND!";
            }
            else
            {
                duckCounterText.text = "Ducks: " + duckCount; // Updated to show "Ducks: #"
            }
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        }
    }

    private void CheckForFastestTime()
    {
        // Load the fastest time from PlayerPrefs
        float fastestTime = PlayerPrefs.GetFloat(fastestTimeKey, float.MaxValue);

        // If the current time is faster, save it as the new fastest time
        if (timer < fastestTime)
        {
            PlayerPrefs.SetFloat(fastestTimeKey, timer);
            PlayerPrefs.Save(); // Save to disk
            LoadFastestTime(); // Update the UI
        }
    }

    private void LoadFastestTime()
    {
        if (fastestTimeText != null)
        {
            float fastestTime = PlayerPrefs.GetFloat(fastestTimeKey, float.MaxValue);

            if (fastestTime == float.MaxValue)
            {
                fastestTimeText.text = "Fastest Time: --:--";
            }
            else
            {
                int minutes = Mathf.FloorToInt(fastestTime / 60f);
                int seconds = Mathf.FloorToInt(fastestTime % 60f);
                fastestTimeText.text = string.Format("Fastest Time: {0:00}:{1:00}", minutes, seconds);
            }
        }
    }
}