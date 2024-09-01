using UnityEngine;
using System;

public class GameStartsDisabler : MonoBehaviour
{
    // Define events for enable and disable
    public static event Action OnGameStartsElementEnabled;
    public static event Action OnGameStartsElementDisabled;

    void OnEnable()
    {
        // Check if this GameObject has the tag "GameStarts"
        if (gameObject.CompareTag("GameStarts"))
        {
            // Trigger the enable event if there are any subscribers
            OnGameStartsElementEnabled?.Invoke();
        }
    }

    void OnDisable()
    {
        // Check if this GameObject has the tag "GameStarts"
        if (gameObject.CompareTag("GameStarts"))
        {
            // Trigger the event if there are any subscribers
            OnGameStartsElementDisabled?.Invoke();
        }
    }
}
