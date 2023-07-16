using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Timer script to create a timer for use in scenes.
 */
public class TimerScript : MonoBehaviour
{

    public static float timer =0.0f; //static variable that can be accessed from other scripts.
    // Start is called before the first frame update
    void Start()
    {
        timer = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
}
