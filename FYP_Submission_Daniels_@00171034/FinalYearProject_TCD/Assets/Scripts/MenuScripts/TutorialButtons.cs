using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This class is used to create a flashing effect for the tutorial UI overlay to draw the user's attention
 * to the image.
 */
public class TutorialButtons : MonoBehaviour
{
    [SerializeField] private GameObject hints;
   
    
    private float timer;
    private bool isVisible = false;
    

        // Update is called once per frame
        void Update()
    {
        timer += Time.deltaTime;
        
        if(timer >=0.75)
        {
            isVisible = !isVisible;
            hints.SetActive(isVisible);
            timer = 0;
        }
    }
    
}
