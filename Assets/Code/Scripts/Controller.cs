using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
   
    public GameObject selectedPlayer;
    

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

