using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color color = default(Color))
    {
        if (color == null)
        {
            color = Color.white;
        }
        return CreateTextMesh(text, parent, localPosition, fontSize, color);
    }
    
    public static TextMesh CreateTextMesh(string text, Transform parent, Vector3 localPosition, int fontSize, Color color)
    {
        GameObject obj = new GameObject("World text", typeof(TextMesh));
        Transform transform = obj.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = obj.GetComponent<TextMesh>();
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;

        return textMesh;
    }

}
