using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayNightManager : MonoBehaviour
{
    public float curTime;
    public int curDay;
    public int curMonth;
    public int curYear;

    public string[] months;

    public TextMeshProUGUI curTime_Text;
    string minutes;
    public TextMeshProUGUI curDay_Text;

    public float dayLength;

    [SerializeField] float smooth;
    public float maxSmooth;

    public Transform sun;

    private void Start()
    {
        StartCoroutine("RotateSun");
    }


    IEnumerator RotateSun()
    {
        #region Date
        if (curDay < 31)
        {
            curDay++;

        }
        else
        {
            curDay = 1;
            if(curMonth < 11)
            {
                curMonth++;
            }
            else
            {
                curMonth = 0;
                curYear++;
            }
            
        }
        curDay_Text.text = curDay.ToString() + " . " + months[curMonth] + " . " + curYear.ToString();
        #endregion

        #region Time

        smooth = curTime;

        while(smooth < maxSmooth)
        {
            float x = smooth % 1;
            
            if (x > .25f && x < .5f)
                minutes = " : 15";
            else if (x > .5f && x < .75f)
                minutes = " : 30";
            else if (x > .75f && x < .99f)
                minutes = " : 45";
            else
                minutes = " : 00";
            


            sun.rotation = Quaternion.Euler(Vector3.Lerp(new Vector3(-90, 0f, 0f), new Vector3(270f, 0f, 0f), smooth / maxSmooth));
            smooth += dayLength;
            yield return null;

            curTime_Text.text = ((int)smooth).ToString() + minutes;


        }
        #endregion

        // Grow Crops;
        GameManager.acc.GL.GrowCrops();

        StartCoroutine("RotateSun");

    }

}
