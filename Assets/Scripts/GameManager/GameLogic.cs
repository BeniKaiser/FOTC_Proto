using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public void InteractWithCurObject(GameObject curObject)
    {
        if(GameManager.acc.I.LetterInput() == "E")
            switch (curObject.tag)
            {
                // Resource Collection
                case "Tree":
                    break;

                case "Rock":
                    break;

                case "Farm":
                    break;

                // Verarbeitung
                case "Kitchen":
                    break;

                case "Forge":
                    break;

                case "Sawmill":
                    break;
            }

    }

    public void OpenInventory()
    {

    }

    void PlantCrop()
    {

    }

    void CollectRessource()
    {

    }
}
