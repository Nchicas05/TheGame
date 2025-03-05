using UnityEngine;

public class VRDrawableTexture : MonoBehaviour
{
    public Transform markerTip; // Assign marker
    public int textureSize = 512;
    public float brushSize = 5f; // Size of the brush stroke

    private Texture2D texture;
    private Color drawColor = Color.red; 
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // Get the existing texture if available
        Texture2D existingTexture = rend.material.mainTexture as Texture2D;

        if (existingTexture != null)
        {
            // Create a new texture with the same size and format as the existing one
            texture = new Texture2D(existingTexture.width, existingTexture.height, existingTexture.format, false);

            // Copy pixels from the existing texture
            texture.SetPixels(existingTexture.GetPixels());
            texture.Apply();
        }
        else
        {
            // Fallback: Create a blank texture
            texture = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;
            ClearTexture();
        }

        // Apply texture to material
        rend.material.mainTexture = texture;
    }


    void Update()
    {
        if (markerTip != null) // Ensure marker exists
        {
            Vector2 uv;
            if (GetUVPoint(markerTip.position, out uv))
            {
                DrawOnTexture(uv);
            }
        }
    }

    void ClearTexture()
    {
        Color[] clearPixels = new Color[textureSize * textureSize];
        for (int i = 0; i < clearPixels.Length; i++) clearPixels[i] = Color.white;
        texture.SetPixels(clearPixels);
        texture.Apply();
    }

    void DrawOnTexture(Vector2 uv)
    {
        int x = (int)(uv.x * textureSize);
        int y = (int)(uv.y * textureSize);

        for (int i = -Mathf.FloorToInt(brushSize / 2); i < Mathf.CeilToInt(brushSize / 2); i++)
        {
            for (int j = -Mathf.FloorToInt(brushSize / 2); j < Mathf.CeilToInt(brushSize / 2); j++)
            {
                int drawX = x + i;
                int drawY = y + j;

                if (drawX >= 0 && drawX < textureSize && drawY >= 0 && drawY < textureSize)
                {
                    texture.SetPixel(drawX, drawY, drawColor);
                }
            }
        }

        texture.Apply();
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
