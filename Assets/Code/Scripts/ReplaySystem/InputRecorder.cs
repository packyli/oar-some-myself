﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRecorder : MonoBehaviour
{
    //private Dictionary<float, PlayerInputStruct> playerInputRecord;
    public Dictionary<float, PlayerInputStruct> playerInputRecord;

    void Start()
    {
        //Intialize the queue that will be used to record inputs
        playerInputRecord = new Dictionary<float, PlayerInputStruct>();
       
    }

    //Adds the timeStamp and playerInputs into the dictionary
    //The timeStamp is the key
    //The inputStruct (inputs) is the value of the key
    //This function is used by the actorObject script as the dictionary is private
    public void AddToDictionary(float time, PlayerInputStruct inputs)
    {
        playerInputRecord.Add(time, inputs);
    }

    public void ClearHistory()
    {
        playerInputRecord = new Dictionary<float, PlayerInputStruct>();
    }
    
    //Check if key exists
    public bool KeyExists(float key)
    {
        return playerInputRecord.ContainsKey(key);
    }

    public bool KeyExistsInRange(float time, float tolerance)
    {
        foreach (var key in playerInputRecord.Keys)
        {
            if (Mathf.Abs(key - time) < tolerance)
            {
                return true;
            }
        }
        return false;
    }

    //Returns the inputStruct at current timeStamp(in)
    public PlayerInputStruct GetRecordedInputs(float timeStamp)
    {
        return playerInputRecord[timeStamp];
    }

    public void PrintInputRecord()
    {
        // Print the input record in the console
        foreach (var entry in playerInputRecord)
        {
            if (entry.Value.buttonPressed)
            {
                Debug.Log($"Time: {entry.Key}, Inputs: RowPace = {entry.Value.rowPaceInput}, " +
                $"RowPower = {entry.Value.rowPowerInput}, " +
                $"RowDistance = {entry.Value.rowDistanceInput}, " +
                $"ButtonPressed = {entry.Value.buttonPressed}");
            }
        }
    }
}
