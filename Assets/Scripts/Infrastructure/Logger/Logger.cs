using UnityEngine;

namespace Infrastructure
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Debug.Log(message);
        }

        public static void LogWarning(string warning)
        {
            Debug.LogWarning(warning);
        }

        public static void LogError(string error)
        {
            Debug.LogError(error);
        }
    }
}
