using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum playerState {normal, inInv }
public class GameManager : MonoBehaviour
{
    public static GameManager acc;
    public playerState curState;

    public PManager PM;
    public InputManager I;
    public GameLogic GL;
    public UILogic UIL;

    public GameObject curObject;

    private void Awake()
    {
        acc = this;
    }

    void Update()
    {
        PM.rb.angularVelocity = Vector3.zero;

        // Set Current Object
        curObject = PM.Col.CurrentObject();

        // CamMovement
        PM.Cam.LookAround(I.CamInput());

        if(curObject != null)
        {
            GL.InteractWithCurObject(curObject);
        }

    }

    private void FixedUpdate()
    {

        //Player Movement
        PM.Move.MovePlayer(PM.rb, I.MovementInput());
    }

    
}
