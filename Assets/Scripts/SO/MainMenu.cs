using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject popupPanel; // Drag panel ini dari inspector

    public void PlayGame()
    {
        popupPanel.SetActive(true); // Tampilkan pop-up
    }

    public void ConfirmStartGame()
    {
        SceneManager.LoadScene(1); // Ganti 1 dengan nama/index scene kamu
    }

    public void ExitGame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();
    }
}
