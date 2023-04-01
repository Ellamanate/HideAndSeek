using System.Linq;
using UnityEngine;

namespace HideAndSeek
{
    public static class GameLogger
    {
        public static void Log(params object[] message)
        {
            Debug.Log(ToLog(message));
        }

        public static void LogWarning(params object[] warning)
        {
            Debug.LogWarning(ToLog(warning));
        }

        public static void LogError(params object[] error)
        {
            Debug.LogError(ToLog(error));
        }

        public static void DrawRay(Vector3 start, Vector3 direction)
        {
            DrawRay(start, direction, Color.red, 0);
        }

        public static void DrawRay(Vector3 start, Vector3 direction, Color color, float duration)
        {
            Debug.DrawRay(start, direction, color, duration);
        }

        public static void DrawLine(Vector3 start, Vector3 end)
        {
            DrawLine(start, end, Color.red, 0);
        }

        public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
        {
            Debug.DrawLine(start, end, color, duration);
        }

        private static string ToLog(params object[] message)
        {
            return message.Aggregate(string.Empty, (current, part) => string.Join(current, part.ToString()));
        }
    }
}
