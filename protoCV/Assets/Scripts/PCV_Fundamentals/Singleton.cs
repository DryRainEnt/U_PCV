using UnityEngine;

namespace PCV_Fundamentals
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance => instance ??= FindObjectOfType<T>();
        private static T instance;
    }
}
