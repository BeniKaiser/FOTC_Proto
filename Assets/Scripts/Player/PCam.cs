using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCam : MonoBehaviour
{
    public Transform cam;
    public float mouseSensitivity;

    public void LookAround(Vector2 mouseInput)
    {
        if(mouseInput.magnitude != 0)
        {
            Vector2 mouseOutput = mouseInput * mouseSensitivity * Time.deltaTime;
            cam.Rotate(new Vector3(-mouseInput.y, 0f, 0f));
            transform.Rotate(new Vector3(0f, mouseInput.x, 0f));
        }
    }
}
