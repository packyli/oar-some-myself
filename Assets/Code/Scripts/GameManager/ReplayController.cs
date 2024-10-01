using System;
using UnityEngine;

public class ReplayController : MonoBehaviour
{
    public GameObject selectedPlayer;

    void OnEnable()
    {
        // Subscribe to the event
        GameEndsEnabler.OnGameEndsElementEnabled += HandleGameEndsElementEnabled;
        GameStartsDisabler.OnGameStartsElementDisabled += HandleGameStartsElementDisabled;
    }

    void OnDisable()
    {
        // Unsubscribe from the event to avoid memory leaks
        GameEndsEnabler.OnGameEndsElementEnabled -= HandleGameEndsElementEnabled;
        GameStartsDisabler.OnGameStartsElementDisabled -= HandleGameStartsElementDisabled;
    }

    void HandleGameEndsElementEnabled()
    {
        // Code to execute when the GameEnds UI element is enabled
        Debug.Log("The GameEnds UI element was enabled!");
        ResetPlayer();
    }

    void HandleGameStartsElementDisabled()
    {
        // Code to execute when the GameStarts UI element is disabled
        Debug.Log("The GameStarts UI element was disabled!");
        StartRecording();
    }

    public void StartRecording()
    {
        ResetPlayer();
        selectedPlayer.GetComponent<ActorObject>().Recording();
    }

    public void StartPlayback()
    {
        ResetPlayer();
        selectedPlayer.GetComponent<ActorObject>().Playback();
    }

    public void ResetPlayer()
    {
        if (selectedPlayer != null)
        {
            selectedPlayer.GetComponent<ActorObject>().Reset();
        }
        else
        {
            Debug.LogWarning("selectedPlayer is null or has been destroyed.");
        }
    }

}
