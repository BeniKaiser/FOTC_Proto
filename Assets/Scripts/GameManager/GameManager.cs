using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PManager PMng;
    public InputManager Inp;

    public GameObject currentObject;

    
    void Start()
    {
        
    }

    void Update()
    {
        PMng.rb.angularVelocity = Vector3.zero;

        // Set Current Object
        currentObject = PMng.Col.CurrentObject();

        // CamMovement
        PMng.Cam.LookAround(Inp.CamInput());

    }

    private void FixedUpdate()
    {

        //Player Movement
        PMng.Move.MovePlayer(PMng.rb, Inp.MovementInput());
    }

    
}
