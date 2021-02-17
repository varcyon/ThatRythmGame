using UnityEngine;

public class InputController : MonoBehaviour {
    [SerializeField] private InputMapper[] mappings;

    void Start() { // Fetches all InputMappers attached to this game object for reference
        mappings = GetComponents<InputMapper>();
    }

    void Update() { // Checks if each button is being pressed using the mappers and runs the attached functions
        foreach (InputMapper mapping in mappings) {
            if (Input.GetButtonDown(mapping.keyName)) {
                mapping.functions.Invoke();
            }
        }
    }
}