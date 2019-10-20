namespace AllForOne
{
    public class SceneManager : Singleton<SceneManager>
    {
        public void LoadScene(int level) => UnityEngine.SceneManagement.SceneManager.LoadScene(level);

        public void LoadScene(string level) => UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }
}