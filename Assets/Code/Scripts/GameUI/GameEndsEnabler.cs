using System;
using UnityEngine;

public class GameEndsEnabler : MonoBehaviour
{
    // Define events for enable and disable
    public static event Action OnGameEndsElementEnabled;
    public static event Action OnGameEndsElementDisabled;

    void OnEnable()
    {
        // Check if this GameObject has the tag "GameEnds"
        if (gameObject.CompareTag("GameEnds"))
        {
            // Trigger the enable event if there are any subscribers
            OnGameEndsElementEnabled?.Invoke();
        }
    }

    void OnDisable()
    {
        // Check if this GameObject has the tag "GameEnds"
        if (gameObject.CompareTag("GameEnds"))
        {
            // Trigger the event if there are any subscribers
            OnGameEndsElementDisabled?.Invoke();
        }
    }
}
