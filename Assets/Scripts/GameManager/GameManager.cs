using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum playerState {normal, inInv, atKitchen, atSawmill, atForge, inShop }
public class GameManager : MonoBehaviour
{

    public static GameManager acc;
    public playerState curState;

    public PManager PM;
    public InputManager I;
    public GameLogic GL;
    public UILogic UIL;
    public DayNightManager DNM;

    public GameObject curObject;

    private void Awake()
    {
        acc = this;
    }

    public void CursorState(CursorLockMode lockMode, bool visibility)
    {
        Cursor.lockState = lockMode;
        Cursor.visible = visibility;
    }


    void PlayerStatics()
    {
        PM.rb.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        if (I.MouseScroll() != 0)
            UIL.ToolRotation((int)I.MouseScroll());

        PlayerStatics();

        switch (curState)
        {
            case playerState.normal:

                // Set Current Object
                curObject = PM.Col.CurrentObject();

                // CamMovement
                PM.Cam.LookAround(I.CamInput());


                InputToOutput();

                break;

            case playerState.inInv:

                InputToOutputInventory();

                break;

            case playerState.atKitchen:
                InputToOutputInventory();
                break;

            case playerState.atSawmill:
                InputToOutputInventory();
                break;

            case playerState.atForge:
                InputToOutputInventory();
                break;

        }
    }

    private void FixedUpdate()
    {
        switch (curState)
        {
            case playerState.normal:
                //Player Movement
                PM.Move.MovePlayer(PM.rb, I.MovementInput());
                break;

            case playerState.inInv:
                break;

        }
    }

    void InputToOutput()
    {
        switch (I.LetterInput())
        {
            case "E":
                if(curObject != null)
                    GL.Interaction(curObject);
                break;

            case "I":
                UIL.InventoryHandling(0);
                break;
        }
    }

    void InputToOutputInventory()
    {
        switch (I.LetterInput())
        {
            case "I":
                UIL.InventoryHandling(0);
                break;
        }

        if(curState == playerState.inInv)
            UIL.FlipDirection(I.LetterInput());
    }

}
