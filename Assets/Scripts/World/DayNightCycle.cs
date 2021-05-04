using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Transform sun;
    public float maxTime;
    public float time;

    public float dayLenght;

    public int curDay;
    public int curTime;

    public Transform farms;

    public void DayNightRythm()
    {
        sun.localRotation = Quaternion.Euler(Mathf.Lerp(0f, 360f, time / maxTime) - 90, 0f, 0f);
        curTime = (int)((time / maxTime) * 24);

        if (time != maxTime)
        {
            time += dayLenght;
            
        }  
        else if (time >= maxTime)
        {
            time = 0;
            curDay++;
            GrowCrops();
        }
            
    }

    void GrowCrops()
    {
        if(farms.childCount > 0)
        {
            for (int i = 0; i < farms.childCount; i++)
            {
                if(farms.GetChild(i).childCount > 0)
                {
                    if (farms.GetChild(i).GetChild(0).GetComponent<CropObject>().growState < 3)
                    {
                        farms.GetChild(i).GetChild(0).GetComponent<CropObject>().growState++;
                        farms.GetChild(i).GetChild(0).position += new Vector3(0f, .3f, 0f);
                    }
                    else if (farms.GetChild(i).GetChild(0).GetComponent<CropObject>().growState == 3)
                    {
                        farms.GetChild(i).GetChild(0).GetComponent<CropObject>().ripe = true;
                        //change Mat;
                    }
                }
                
            }
        }
        
    }
}
