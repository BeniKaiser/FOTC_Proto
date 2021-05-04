using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform cam;
    [SerializeField] float mouseSensitivity;

    public void RotateCam(Vector2 mouseInput)
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Vector2 mouseOutput = mouseInput * mouseSensitivity * Time.deltaTime;
            cam.localRotation *= Quaternion.Euler(-mouseOutput.y, 0f, 0f);
            transform.localRotation *= Quaternion.Euler(0f, mouseOutput.x, 0f);
        }
        
    }
}
