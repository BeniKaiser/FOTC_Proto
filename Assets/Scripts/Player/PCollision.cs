using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCollision : MonoBehaviour
{
    [SerializeField] LayerMask interactable_Msk;
    [SerializeField] float checkLength;
    public GameObject CheckForward(Transform cam)
    {
        RaycastHit[] hit = Physics.RaycastAll(transform.position, cam.forward, checkLength, interactable_Msk);
        if (hit.Length > 0)
        {
            return hit[0].transform.gameObject;
        }
        return null;
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Item"))
        {
            PManager.acc.Inv.AddToInv(col.gameObject, 0, false);
            Destroy(col.gameObject);
        }
        else if (col.gameObject.CompareTag("Crop"))
        {
            PManager.acc.Inv.AddToInv(col.gameObject, 0, true);
            Destroy(col.gameObject);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, GetComponent<CamMove>().cam.forward * checkLength);
    }
}
