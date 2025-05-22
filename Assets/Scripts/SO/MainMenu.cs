using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Panggil ini dari tombol Play
    public void PlayGame()
    {
        // Ganti 1 dengan index atau nama scene awal game
        SceneManager.LoadScene(1);
    }

    // Panggil ini dari tombol Exit
    public void ExitGame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit(); // Akan bekerja saat build game (tidak di editor)
    }
}
