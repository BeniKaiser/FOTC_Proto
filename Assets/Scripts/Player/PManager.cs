using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PManager : MonoBehaviour
{
    public static GameObject player;
    public static PManager acc;

    private void Awake()
    {
        player = gameObject;
        acc = this;
    }

    public PMove Move;
    public PCam Cam;
    public PJump jump;
    public PCollision Col;

    public Rigidbody rb;

    
}
