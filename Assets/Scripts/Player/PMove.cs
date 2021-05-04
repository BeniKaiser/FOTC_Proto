using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMove : MonoBehaviour
{
    public float speed;

    public void Move(Vector2 moveInput)
    {
        Vector2 moveOutput = moveInput * speed * Time.fixedDeltaTime;
        PManager.acc.rb.velocity = transform.forward * moveOutput.y + transform.right * moveOutput.x;
    }
}
