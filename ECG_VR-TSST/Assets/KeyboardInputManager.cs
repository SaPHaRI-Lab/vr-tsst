using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyboardInputManager : MonoBehaviour {

    [SerializeField]
    private Button weiter;
    [SerializeField]
    private Button erneut;
    [SerializeField]
    private Button zeitNichtUm;
    [SerializeField]
    private Button nichtVerstanden;
    [SerializeField]
    private Button lautUndDeutlich;
    [SerializeField]
    private Button vonVorne;
    [SerializeField]
    private Button falsch;
    [SerializeField]
    private Button richtig;
    [SerializeField]
    private Button ergebnis;

    /// <summary>
    /// Listen for the number keys one through nine and initiate button presses accordingly
    /// </summary>
    public void Update()
    {
        if (Keyboard.current[Key.Digit1].wasPressedThisFrame)
        {
            weiter.onClick.Invoke();
        }
        else if (Keyboard.current[Key.Digit2].wasPressedThisFrame)
        {
            erneut.onClick.Invoke();
        }
        else if (Keyboard.current[Key.Digit3].wasPressedThisFrame)
        {
            zeitNichtUm.onClick.Invoke();
        }
        else if (Keyboard.current[Key.Digit4].wasPressedThisFrame)
        {
            nichtVerstanden.onClick.Invoke();
        }
        else if (Keyboard.current[Key.Digit5].wasPressedThisFrame)
        {
            lautUndDeutlich.onClick.Invoke();
        }
        else if (Keyboard.current[Key.Digit6].wasPressedThisFrame)
        {
            vonVorne.onClick.Invoke();
        }
        else if (Keyboard.current[Key.Digit7].wasPressedThisFrame)
        {
            falsch.onClick.Invoke();
        }
        else if (Keyboard.current[Key.Digit8].wasPressedThisFrame)
        {
            richtig.onClick.Invoke();
        }
        else if (Keyboard.current[Key.Digit9].wasPressedThisFrame)
        {
            ergebnis.onClick.Invoke();
        }
    }
}
