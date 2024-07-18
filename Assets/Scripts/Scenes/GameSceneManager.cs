using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class GameSceneManager : MonoBehaviour
    {
        public void LoadGameScene()
        {
            SceneManager.LoadSceneAsync(GlobalConstants.GAME_SCENE);
        }
    }
}
