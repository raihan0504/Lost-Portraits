using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject uiToHide;

    void Start()
    {
        if (timeline != null)
        {
            timeline.stopped += OnTimelineStopped;
        }
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        if (uiToHide != null)
        {
            uiToHide.SetActive(false);
            Debug.Log("UI disembunyikan karena timeline selesai.");
        }
    }
}
