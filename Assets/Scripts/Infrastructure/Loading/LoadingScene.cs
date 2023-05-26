using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class LoadingScene
    {
        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
