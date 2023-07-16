using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script to handle clock time, sets it to real world time
 */
public class Clock : MonoBehaviour
{
    // fields to hols clock hand objects
    [SerializeField] private GameObject secondHand;
    [SerializeField] private GameObject minuteHand;
    [SerializeField] private GameObject hourHand;    
    public AudioManager audioMan;
    string lastSecond;

    
    // Update is called once per frame
    void Update()
    {
        string seconds = System.DateTime.UtcNow.ToString("ss");

        if (seconds != lastSecond) //Checks the current second is different than the previous one, calls method to update time
        {            
            audioMan.PlayOnObject("ClockTick", gameObject);   
            UpdateTimer();
        }
        lastSecond = seconds;
    }

    /**
     * Retrieves relevant time information and updates the clock hands accordingly
     */
    void UpdateTimer()
    {
        int secondsInt = int.Parse(System.DateTime.UtcNow.ToString("ss"));
        int minuteInt = int.Parse(System.DateTime.UtcNow.ToString("mm"));
        int hourInt = int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("hh"));

        secondHand.transform.rotation = Quaternion.Euler(-secondsInt * 6, 0, 0);
        minuteHand.transform.rotation = Quaternion.Euler(-minuteInt * 6, 0, 0);
        float hourInterval = (float)(minuteInt) / 60f;
        


        hourHand.transform.rotation = Quaternion.Euler(-((float)hourInt + hourInterval) * 360 / 12, 0, 0);
    }
}
