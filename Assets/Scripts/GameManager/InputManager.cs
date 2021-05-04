using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public string KeyboardInput()
    {
        string keyInput = Input.inputString.ToUpper();
        return keyInput;
    }

    public Vector2 MoveInput()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        return moveInput;
    }

    public Vector2 MouseInput()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        return mouseInput;
    }

    public int ScrollInput()
    {
        int scrollInput = (int)Input.mouseScrollDelta.y;
        return scrollInput;
    }
}
