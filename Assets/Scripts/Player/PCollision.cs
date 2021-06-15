using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PCollision : MonoBehaviour
{
    // Debug
    public TextMeshProUGUI facingObject_text;
    public float rayLength;

    public RaycastHit hit;

    public GameObject CurrentObject()
    {
        if (Physics.Raycast(transform.position, GetComponent<PCam>().cam.forward, out hit, rayLength))
        {
            facingObject_text.text = hit.transform.gameObject.name + " (" + hit.transform.gameObject.tag + ")";
            return hit.transform.gameObject;
        }
        facingObject_text.text = "-null-";
        return null;
    }


    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.GetComponent<DropProperties>() != null)
        {
            GameManager.acc.GL.CollectObject(col.gameObject);
            Destroy(col.gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Balloon"))
        {
            GameManager.acc.curState = playerState.inMap;
            PManager.acc.rb.velocity = Vector3.zero;
            GameManager.acc.UIL.balloonCanvas.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, GetComponent<PCam>().cam.forward * rayLength);
    }
}
