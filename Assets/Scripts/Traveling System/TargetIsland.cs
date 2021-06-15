using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetIsland : MonoBehaviour, IPointerClickHandler   
{
    public Transform airBalloon;
    public Transform islandLandingPoint;

    public void OnPointerClick(PointerEventData pointer)
    {
        print("Travel Here");

        airBalloon.position = islandLandingPoint.position;
        PManager.player.transform.position = airBalloon.position;

        GameManager.acc.UIL.balloonCanvas.SetActive(false);
        GameManager.acc.curState = playerState.normal;
        GameManager.acc.CursorState(CursorLockMode.Locked, false);
    }

    

}
