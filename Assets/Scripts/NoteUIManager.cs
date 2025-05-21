using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteUIManager : MonoBehaviour
{
    public static NoteUIManager Instance;

    [Header("UI Components")]
    public GameObject notePanel;
    public TextMeshProUGUI noteText; // Atau pakai Text biasa
    public Button closeButton;

    [Header("Other UI")]
    public GameObject toolbar;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        closeButton.onClick.AddListener(HideNote);
        notePanel.SetActive(false);
    }

    public void ShowNote(string content)
    {
        noteText.text = content;
        notePanel.SetActive(true);
        if (toolbar != null) toolbar.SetActive(false);
        Time.timeScale = 0f; // Pause game saat baca
    }

    public void HideNote()
    {
        notePanel.SetActive(false);
        if (toolbar != null) toolbar.SetActive(true);
        Time.timeScale = 1f;
    }
}
