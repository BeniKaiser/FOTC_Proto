using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PCollision : MonoBehaviour
{
    // Debug
    public TextMeshProUGUI facingObject_text;
    public float rayLength;

    RaycastHit hit;

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

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, GetComponent<PCam>().cam.forward * rayLength);
    }
}
