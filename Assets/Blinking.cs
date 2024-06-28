using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    public Renderer characterRenderer; // Assign this in the Inspector
    public bool isBlinking = false;
    public float blinkSpeed = 1.0f;

    private Material characterMaterial;
    private Color originalColor;

    void Start()
    {
        characterMaterial = characterRenderer.material;
        originalColor = characterMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlinking)
        {
            float alpha = Mathf.PingPong(Time.time * blinkSpeed, 0.8f) + 0.2f; // PingPong between 0.2 and 1.0
            Color newColor = originalColor;
            newColor.a = alpha;
            characterMaterial.color = newColor;
        }
        else
        {
            characterMaterial.color = originalColor; // Reset to original color when not blinking
        }
    }
}
