using UnityEngine;

public class VRDrawableTexture : MonoBehaviour

    
{
    //assing in inspector
    public Transform markerTip;
    public int textureSize = 512;
    public float brushSize = 5f;
    public AudioClip drawSound; 

    private Texture2D texture;
    private Color drawColor = Color.red;
    private Renderer rend;
    private AudioSource audioSource;
    private bool wasDrawing = false;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // Attach or use existing AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = drawSound;
        audioSource.loop = false; // Ensure it doesn't loop

        // Get existing texture
        Texture2D existingTexture = rend.material.mainTexture as Texture2D;

        if (existingTexture != null)
        {
            texture = new Texture2D(existingTexture.width, existingTexture.height, TextureFormat.RGBA32, false);
            texture.SetPixels(existingTexture.GetPixels());
            texture.Apply();
        }

        rend.material.mainTexture = texture;
    }

    void Update()
    {
        if (markerTip != null)
        {
            Vector2 uv;
            if (GetUVPoint(markerTip.position, out uv))
            {
                DrawOnTexture(uv);
            }
            else
            {
                wasDrawing = false; // Reset when not drawing
            }
        }
    }

    void DrawOnTexture(Vector2 uv)
    {
        int x = (int)(uv.x * textureSize);
        int y = (int)(uv.y * textureSize);
        bool didDraw = false;

        for (int i = -Mathf.FloorToInt(brushSize / 2); i < Mathf.CeilToInt(brushSize / 2); i++)
        {
            for (int j = -Mathf.FloorToInt(brushSize / 2); j < Mathf.CeilToInt(brushSize / 2); j++)
            {
                int drawX = x + i;
                int drawY = y + j;

                if (drawX >= 0 && drawX < textureSize && drawY >= 0 && drawY < textureSize)
                {
                    texture.SetPixel(drawX, drawY, drawColor);
                    didDraw = true; // Something was drawn
                }
            }
        }

        texture.Apply();

        // Play sound if drawing started this frame
        if (didDraw && !wasDrawing)
        {
            audioSource.Play();
        }

        wasDrawing = didDraw;
    }

    bool GetUVPoint(Vector3 worldPosition, out Vector2 uv)
    {
        Ray ray = new Ray(worldPosition, -markerTip.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == rend.GetComponent<Collider>())
            {
                uv = hit.textureCoord;
                return true;
            }
        }

        uv = Vector2.zero;
        return false;
    }
}
