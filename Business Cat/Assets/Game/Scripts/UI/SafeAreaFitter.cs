using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaFitter : MonoBehaviour
{
    private void Awake()
    {
        var rectTransform = GetComponent<RectTransform>();
        var safeArea = UnityEngine.Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = anchorMin + safeArea.size;

        anchorMin.x /= UnityEngine.Screen.width;
        anchorMin.y /= UnityEngine.Screen.height;
        anchorMax.x /= UnityEngine.Screen.width;
        anchorMax.y /= UnityEngine.Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }
}