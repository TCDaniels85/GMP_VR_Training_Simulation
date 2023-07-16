using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles the tablet screen to display social media discrimination.
 * switches screen to social media example when picked up
 */
public class TabletScreen : MonoBehaviour
{
    [SerializeField] private Texture2D[] tabletdisplay;
    [SerializeField] private Renderer tabletScreen;

    // Start is called before the first frame update
    void Start()
    {
        tabletScreen.material.mainTexture = tabletdisplay[0];
        
    }  

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand") && tabletScreen.material.mainTexture == tabletdisplay[0])
        {
            tabletScreen.material.mainTexture = tabletdisplay[1];
            
        }
    }

    

    
}
