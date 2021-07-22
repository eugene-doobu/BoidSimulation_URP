using UnityEngine;
using UnityEngine.SceneManagement;
namespace BoidsSimulationOnGPU
{
    public class ButtonFuncs : MonoBehaviour
    {
        FileManager fileManager;

        private void Start()
        {
            fileManager = LobbyManager.instance.FileManager;
        }

        public void OnLobbyButton()
        {
            SceneManager.LoadScene("1.Scenes/Lobby");
        }

        public void OnSimulationButton()
        {
            SceneManager.LoadScene("1.Scenes/BoidsSimulationOnGPU");
        }

        public void OnExitButton()
        {
            Application.Quit();
        }
    }
}
