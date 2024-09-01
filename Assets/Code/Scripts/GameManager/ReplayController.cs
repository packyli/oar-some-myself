using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayController : MonoBehaviour
{
    public GameObject selectedPlayer;

    private Engine engine;

    private void Awake()
    {
        engine = GetComponent<Engine>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!engine.IsStarted)
        {
            ResetPlayer();
        }
        else if (engine.IsStarted && engine.currentRound == 1)
        {
            StartRecording();
        }
        else if (engine.IsStarted && engine.currentRound > 1)
        {
            StartPlayback();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
