using UnityEngine;
using Cinemachine;

public class ConfinerManager : MonoBehaviour
{
    public static ConfinerManager Instance { get; private set; }

    public CinemachineVirtualCamera virtualCam;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // opsional
    }

    public void SetConfiner(Collider2D newConfiner)
    {
        var confiner = virtualCam.GetComponent<CinemachineConfiner2D>();
        if (confiner != null && newConfiner != null)
        {
            confiner.enabled = false;
            confiner.m_BoundingShape2D = newConfiner;
            confiner.enabled = true;
        }
    }
}
