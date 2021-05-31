using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 MovementInput()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Debug.Log(moveInput);
        return moveInput;
    }

    public Vector2 CamInput()
    {
        Vector2 camInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        return camInput;
    }

    public string LetterInput()
    {
        string keyInput = Input.inputString.ToUpper();
        return keyInput;
    }

    
}
