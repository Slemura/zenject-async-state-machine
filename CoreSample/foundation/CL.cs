using UnityEngine;

public static class Cl {
    
    public static void Log(string text, Color color = default, GameObject context = null) {
        if(color == default) color = Color.cyan;
        Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{text}</color>", context);
    }
}
