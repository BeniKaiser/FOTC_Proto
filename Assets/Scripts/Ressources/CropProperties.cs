using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropProperties : MonoBehaviour
{
    public bool ripe;

    public int daysToMature;
    public int curDay;

    public MeshRenderer mesh;

    public Material growing_Mat, ripe_Mat;

}
