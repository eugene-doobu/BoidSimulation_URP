using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFuncs : MonoBehaviour
{
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
