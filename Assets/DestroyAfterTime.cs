using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndDestroyUniversal : MonoBehaviour
{
    [Tooltip("Seconds to wait before starting the fade")]
    public float waitBeforeFade = 5f;
    [Tooltip("Seconds the fade lasts")]
    public float fadeDuration = 1f;
    [Tooltip("Try to switch Standard shader materials to a transparent mode (may not work for custom/URP shaders)")]
    public bool forceStandardToTransparent = true;

    // collected components
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    private List<Renderer> meshRenderers = new List<Renderer>();
    private List<Material[]> meshMaterialsList = new List<Material[]>();
    private List<Color[]> meshOriginalColors = new List<Color[]>();
    private List<Graphic> uiGraphics = new List<Graphic>();
    private List<Color> spriteOriginalColors = new List<Color>();
    private List<Color> uiOriginalColors = new List<Color>();
    private CanvasGroup canvasGroup;

    void Awake()
    {
        // collect sprite renderers
        spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>(true));
        foreach (var sr in spriteRenderers) spriteOriginalColors.Add(sr.color);

        // collect renderers (excluding SpriteRenderer)
        var allRenderers = GetComponentsInChildren<Renderer>(true);
        foreach (var r in allRenderers)
        {
            if (r is SpriteRenderer) continue;
            meshRenderers.Add(r);

            // create unique material instances so changing alpha doesn't affect other objects
            var mats = r.materials; // returns a copy
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = new Material(mats[i]);
                if (forceStandardToTransparent && mats[i].shader != null && mats[i].shader.name.Contains("Standard"))
                    MakeMaterialTransparent(mats[i]);
            }
            r.materials = mats;

            // store for runtime edits
            meshMaterialsList.Add(mats);

            // store original colors for each material if it has _Color
            Color[] cols = new Color[mats.Length];
            for (int i = 0; i < mats.Length; i++)
                cols[i] = mats[i].HasProperty("_Color") ? mats[i].color : Color.white;
            meshOriginalColors.Add(cols);
        }

        // collect UI Graphics (Image/Text/TMP MaskableGraphic etc)
        uiGraphics.AddRange(GetComponentsInChildren<Graphic>(true));
        foreach (var g in uiGraphics) uiOriginalColors.Add(g.color);

        // CanvasGroup - prefer an existing one on this object; if not and we have UI children, add one here
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null && uiGraphics.Count > 0)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
        }

        Debug.Log($"FadeAndDestroyUniversal: sprites={spriteRenderers.Count}, meshes={meshRenderers.Count}, ui={uiGraphics.Count}");
    }

    void Start()
    {
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        // wait first
        yield return new WaitForSeconds(waitBeforeFade);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            float a = 1f - t; // alpha from 1 -> 0

            // UI - prefer canvasGroup (multiplicative) if present
            if (canvasGroup != null)
            {
                canvasGroup.alpha = a;
            }
            else
            {
                for (int i = 0; i < uiGraphics.Count; i++)
                {
                    Color c = uiOriginalColors[i];
                    c.a = c.a * a;
                    uiGraphics[i].color = c;
                }
            }

            // Sprites
            for (int i = 0; i < spriteRenderers.Count; i++)
            {
                Color c = spriteOriginalColors[i];
                c.a = c.a * a;
                spriteRenderers[i].color = c;
            }

            // Mesh/Skinned renderers' materials
            for (int r = 0; r < meshMaterialsList.Count; r++)
            {
                var mats = meshMaterialsList[r];
                var origCols = meshOriginalColors[r];
                for (int m = 0; m < mats.Length; m++)
                {
                    if (mats[m].HasProperty("_Color"))
                    {
                        Color c = origCols[m];
                        c.a = c.a * a;
                        mats[m].color = c;
                    }
                }
            }

            yield return null;
        }

        // final ensure alpha zero
        if (canvasGroup != null) canvasGroup.alpha = 0f;
        else
        {
            foreach (var g in uiGraphics) { var c = g.color; c.a = 0f; g.color = c; }
        }
        foreach (var sr in spriteRenderers) { var c = sr.color; c.a = 0f; sr.color = c; }
        foreach (var mats in meshMaterialsList)
        {
            for (int i = 0; i < mats.Length; i++)
            {
                if (mats[i].HasProperty("_Color")) { var c = mats[i].color; c.a = 0f; mats[i].color = c; }
            }
        }

        Destroy(gameObject);
    }

    // Helper for Unity's Standard shader to make it support alpha (works for many cases; may not work for URP/custom)
    private void MakeMaterialTransparent(Material m)
    {
        // set Standard shader mode to Fade (2)
        m.SetFloat("_Mode", 2f);
        m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        m.SetInt("_ZWrite", 0);
        m.DisableKeyword("_ALPHATEST_ON");
        m.EnableKeyword("_ALPHABLEND_ON");
        m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        m.renderQueue = 3000;
    }
}
