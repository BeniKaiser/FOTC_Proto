using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PManager : MonoBehaviour
{
    public static GameObject player;

    private void Awake()
    {
        player = gameObject;
    }

    public PMove Move;
    public PCam Cam;
    public PJump jump;
    public PCollision Col;

    public Rigidbody rb;

    
}
