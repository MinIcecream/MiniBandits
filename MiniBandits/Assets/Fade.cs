using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fade : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    public float fadeTime = 3.0f;

    void Awake()
    {
        StartCoroutine(FadeOutCoroutine());
    } 
    private IEnumerator FadeOutCoroutine()
    {
        // Get the current alpha value of the text
        float startAlpha = text.color.a;

        // Loop over time to fade out the text
        for (float t = 0.0f; t < fadeTime; t += Time.deltaTime)
        {
            // Calculate the new alpha value based on the time elapsed
            float newAlpha = Mathf.Lerp(startAlpha, 0.0f, t / fadeTime);

            // Set the new alpha value on the text color
            Color newColor = text.color;
            newColor.a = newAlpha;
            text.color = newColor;

            // Wait for the next frame
            yield return null;
        }

        // Set the final alpha value to 0 to ensure it's fully faded out
        Color finalColor = text.color;
        finalColor.a = 0.0f;
        text.color = finalColor;
        Destroy(gameObject);
    }
}
