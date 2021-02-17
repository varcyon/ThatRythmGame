using UnityEngine;
using UnityEngine.Events;

public class InputMapper : MonoBehaviour {
    public string keyName; // Defines the Input Name seen in the Project Settings
    public UnityEvent functions; // Defines the functions that will be run when button is pressed
}