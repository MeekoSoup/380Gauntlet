using UnityEngine;
using UnityEngine.SceneManagement;

namespace Control
{
    public class SceneUtility : MonoBehaviour
    {
        public static void LoadLevel(string level)
        {
            SceneManager.LoadScene(level);
        }

        public static void LoadLevel(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex);
        }

        public static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                         Application.Quit();
#endif
        }

        public static void Pause()
        {
            Time.timeScale = 0f;
        }

        public static void Unpause()
        {
            Time.timeScale = 1f;
        }

        public static void ReloadLevel()
        {
            LoadLevel(SceneManager.GetActiveScene().name);
        }
    }
}