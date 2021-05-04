using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager acc;

    public InputManager Inp;
    public PManager Player;
    public UIManager UIMng;
    public DayNightCycle DNC;

    bool playerMoves;

    bool invVisibility;
    public GameObject InvUI;
    public GameObject curInteractable;

    //Debug

    [SerializeField] TextMeshProUGUI facingObj_Debug;

    //Debug End

    private void Awake()
    {
        acc = this;
    }

    void Update()
    {
        StaticCommands();

        DNC.DayNightRythm();

        PlayerManagement();
        UIManagement();

        //Debug
        DebugFunc();
    }

    void StaticCommands()
    {
        Player.rb.angularVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        PlayerRigidbody();
    }

    void PlayerManagement()
    {
        // Move bool

        if(Inp.MoveInput().magnitude != 0)
            playerMoves = true;
        else
            playerMoves = false;

        // Rotate Cam

        if (Inp.MouseInput().magnitude != 0)
            Player.Cam.RotateCam(Inp.MouseInput());

        // Interactable Check

        curInteractable = Player.Col.CheckForward(Player.Cam.cam);

        // Interact

        PlayerInputToOutput();
    }

    void Interact()
    {
        if(curInteractable != null)
        {

            switch (curInteractable.tag)
            {
                case "Tree":
                    if(Player.Inv.quickSlotIndex == 0)
                        DamageResource(curInteractable);
                    break;

                case "Rock":
                    if(Player.Inv.quickSlotIndex == 1)
                        DamageResource(curInteractable);
                    break;

                case "Crop":
                    if(Player.Inv.quickSlotIndex == 2)
                        if (curInteractable.GetComponent<CropObject>().ripe)
                            CollectResource(curInteractable);
                    break;

                case "Collectable":
                    CollectResource(curInteractable);
                    break;

                case "Farm":
                    InventoryVisibility(1);
                    break;

                case "Sawmill":
                    InventoryVisibility(2);
                    UIMng.sawmill_UI.SetActive(true);
                    break;

                case "Forge":
                    InventoryVisibility(3);
                    UIMng.forge_UI.SetActive(true);
                    break;

                case "Kitchen":
                    InventoryVisibility(4);
                    UIMng.cooking_UI.SetActive(true);
                    break;
            }
        }
        
    }

    void PlayerInputToOutput()
    {
        switch (Inp.KeyboardInput())
        {
            case "E":
                Interact();
                break;

            case "I":
                InventoryVisibility(0);
                break;

            default:
                break;
        }
    }

    void CollectResource(GameObject obj)
    {
        for (int i = 0; i < obj.GetComponent<ResourceObjects>().dropRate; i++)
        {
            Vector3 rand = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            Instantiate(obj.GetComponent<ResourceObjects>().drop, obj.transform.position + rand, Quaternion.identity);
        }
        Destroy(obj);
    }

    void DamageResource(GameObject obj)
    {
        Debug.Log("Chop");
        if (obj.GetComponent<ResourceObjects>().durability > 0)
        {
            obj.GetComponent<ResourceObjects>().durability--;
        }
        else
        {
            for (int i = 0; i < obj.GetComponent<ResourceObjects>().dropRate; i++)
            {
                Vector3 rand = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
                Instantiate(obj.GetComponent<ResourceObjects>().drop, obj.transform.position + rand, Quaternion.identity);
            }
            Destroy(obj);
        }
            
    }

    void UIManagement()
    {

        if (Inp.ScrollInput() != 0)
            Player.Inv.MoveSelection(Inp.ScrollInput());

    }

    void InventoryVisibility(int invUse) // 0 = normal InvOpen, 1 = Farm, 2 = Sägewerk, 3 = Schmiede, 4 = Kitchen
    {
        switch (invUse)
        {
            case 0:
                InvUI.GetComponent<CurInvType>().curInvType = 0;
                break;
            case 1:
                InvUI.GetComponent<CurInvType>().curInvType = 1;
                break;
            case 2:
                InvUI.GetComponent<CurInvType>().curInvType = 2;
                break;
            case 3:
                InvUI.GetComponent<CurInvType>().curInvType = 3;
                break;
            case 4:
                InvUI.GetComponent<CurInvType>().curInvType = 4;

                break;
        }

        VisibilitySwitch();
    }

    public void VisibilitySwitch()
    {
        switch (invVisibility)
        {
            case true:
                invVisibility = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;

            case false:
                invVisibility = true;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
        }

        InvUI.SetActive(invVisibility);
    }

    void PlayerRigidbody()
    {
        // Move

        if (playerMoves)
            Player.Move.Move(Inp.MoveInput());
    }

    void DebugFunc()
    {
        if (curInteractable != null)
            facingObj_Debug.text = curInteractable.name;
        else
            facingObj_Debug.text = "-null-";
    }
}
