using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMove : MonoBehaviour
{
    public float speed;
    public float maxMagnitude;

    public void MovePlayer(Rigidbody rb, Vector3 moveInput)
    {


        Vector3 moveOutput = (moveInput.y * transform.forward + moveInput.x * transform.right) * speed * Time.fixedDeltaTime;
        rb.velocity = new Vector3(moveOutput.x, rb.velocity.y, moveOutput.z);

    }
}
