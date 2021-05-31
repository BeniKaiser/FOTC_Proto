using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvPageProperties : MonoBehaviour
{
    public void FlipToPage()
    {
        transform.SetAsLastSibling();
    }
}
