using System;
using UnityEngine;

public class ReplayController : MonoBehaviour
{
    public GameObject selectedPlayer;

    void OnEnable()
    {
        // Subscribe to the event
        GameStartsDisabler.OnGameStartsElementEnabled += HandleGameStartsElementEnabled;
        GameStartsDisabler.OnGameStartsElementDisabled += HandleGameStartsElementDisabled;
    }

    void OnDisable()
    {
        // Unsubscribe from the event to avoid memory leaks
        GameStartsDisabler.OnGameStartsElementEnabled -= HandleGameStartsElementEnabled;
        GameStartsDisabler.OnGameStartsElementDisabled -= HandleGameStartsElementDisabled;
    }

    void HandleGameStartsElementEnabled()
    {
        // Code to execute when the GameStarts UI element is enabled
        Debug.Log("The GameStarts UI element was enabled!");
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
        selectedPlayer.GetComponent<ActorObject>().Reset();
    }
}
