using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PManager : MonoBehaviour
{
    public static PManager acc;

    public PMove Move;
    public PInventory Inv;
    public PCollision Col;
    public CamMove Cam;
    public Rigidbody rb;

    private void Awake()
    {
        acc = this;
    }
}
