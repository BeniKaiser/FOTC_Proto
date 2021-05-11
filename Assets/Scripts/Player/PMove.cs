using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMove : MonoBehaviour
{
    public float speed;

    public void MovePlayer(Rigidbody rb, Vector2 moveInput)
    {
        if(moveInput.magnitude != 0)
        {
            Vector3 moveOutput = moveInput.y * transform.forward + moveInput.x * transform.right;
           
            rb.velocity = moveOutput * speed * Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
        
    }
}
