using UnityEngine;
using UnityEngine.SceneManagement;
namespace BoidsSimulationOnGPU
{
    public class ButtonFuncs : MonoBehaviour
    {
        [SerializeField] GameObject modal;

        public void OnLobbyButton()
        {
            SceneManager.LoadScene("1.Scenes/Lobby");
        }

        public void OnSimulationButton()
        {
            SceneManager.LoadScene("1.Scenes/BoidsSimulationOnGPU");
        }

        public void OnSettingButton()
        {
            // ���â�� ����â Ȱ��ȭ
            modal?.SetActive(true);
            modal?.transform.Find("Panel_Settings").gameObject.SetActive(true);
        }

        public void OnManualButton()
        {
            // ���â�� ����â Ȱ��ȭ
            modal?.SetActive(true);
            modal?.transform.Find("Panel_KeyMenual").gameObject.SetActive(true);
        }

        public void OnExitButton()
        {
            Application.Quit();
        }
    }
}
