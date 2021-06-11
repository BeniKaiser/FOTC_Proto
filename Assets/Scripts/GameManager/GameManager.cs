using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum playerState {normal, inInv, inQuestLog, atKitchen, atSawmill, atForge, inShop, atQuestGiver }
public class GameManager : MonoBehaviour
{

    public static GameManager acc;
    public playerState curState;

    public PManager PM;
    public InputManager I;
    public InventoryManager IM;
    public GameLogic GL;
    public UILogic UIL;
    public DayNightManager DNM;

    public GameObject curObject;

    private void Awake()
    {
        acc = this;

        //testing
        GameLogic.pMoney = 100;
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
        GL.ManageMoney();

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
                UIL.FlipDirection(I.LetterInput());

                break;

            case playerState.inQuestLog:
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

            case playerState.inShop:
                InputToOutputInventory();
                UIL.FlipDirection(I.LetterInput());
                break;

            case playerState.atQuestGiver:
                CursorState(CursorLockMode.Confined, true);
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

            case "L":
                UIL.QuestLogHandling();
                break;
        }
    }

    void InputToOutputInventory()
    {
        switch (I.LetterInput())
        {

            case "I":
                if(!UIL.questLog.activeSelf)
                    UIL.InventoryHandling(0);
                break;

            case "L":
                if(!UIL.inventory.activeSelf)
                    UIL.QuestLogHandling();
                break;
        }

    }

}
