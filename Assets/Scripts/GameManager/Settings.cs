using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] int fps;
    [SerializeField] bool runInBack;

    private void Awake()
    {
        Application.targetFrameRate = fps;
        Application.runInBackground = runInBack;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
