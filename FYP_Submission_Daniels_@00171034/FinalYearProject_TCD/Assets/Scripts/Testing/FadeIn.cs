using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Creates a fade in effect to hide player body while the scene is beinning and to ease player into scene.
 */
public class FadeIn : MonoBehaviour
{
    public Image image;
    
    // Start is called before the first frame update
    void Awake()
    {
        
        StartCoroutine(CrossFade());
    }

    IEnumerator CrossFade()
    {
        yield return new WaitForSeconds(2); //waits for 2 seconds before fading in.
        image.CrossFadeAlpha(0, 2.0f, false);        
        Destroy(gameObject,3.0f);
    }

}
