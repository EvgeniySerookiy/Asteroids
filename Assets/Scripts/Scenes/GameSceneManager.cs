using UnityEngine.SceneManagement;

namespace Scenes
{
    public class GameSceneManager
    {
        public void LoadGameScene()
        {
            SceneManager.LoadSceneAsync(GlobalConstants.GAME_SCENE);
        }
    }
}
