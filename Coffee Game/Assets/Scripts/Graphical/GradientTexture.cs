using System;
using Unity.Collections;
using UnityEngine;

public class GradientTexture : MonoBehaviour
{
    [SerializeField] private Gradient grad;
    private SpriteRenderer _sr;
    private int move = 0;
    private NativeArray<Color32> raw;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        Texture2D tex = new Texture2D(256, 256, TextureFormat.RGBA32, 0, true);
        raw = tex.GetRawTextureData<Color32>();
        float time = 0f;
        for (int i = 0; i < 256; i++)
        {
            time = i / 256f;
            
            Color32 col = grad.Evaluate(time);
            if (i == 0 || i == 255)
            {
                Debug.Log($"color at {i}: {col}");
            }
            for (int o = 0; o < 256; o++)
            {
                raw[i * 256 + o] = col;
            }
        }
        tex.LoadRawTextureData(raw);
        tex.Apply();

        _sr.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 256);
    }

    private void Update()
    {
        Texture2D tex = new Texture2D(256, 256, TextureFormat.RGBA32, 0, true);
        raw = tex.GetRawTextureData<Color32>();
        float time = 0f;
        for (int i = 0; i < 256; i++)
        {
            time = (i + move) % 256 / 256f;
            
            Color32 col = grad.Evaluate(time);
            for (int o = 0; o < 256; o++)
            {
                raw[i * 256 + o] = col;
            }
        }

        move++;
        tex.LoadRawTextureData(raw);
        tex.Apply();

        _sr.material.mainTexture = tex;
    }
}
