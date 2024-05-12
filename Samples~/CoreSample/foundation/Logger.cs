using UnityEngine;

namespace RpDev.AsyncStateMachine.Sample
{
    public static class Logger
    {
        public static void Log(string text, Color color = default, GameObject context = null)
        {
            if (color == default) color = Color.red;
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{text}</color>", context);
        }
    }
}