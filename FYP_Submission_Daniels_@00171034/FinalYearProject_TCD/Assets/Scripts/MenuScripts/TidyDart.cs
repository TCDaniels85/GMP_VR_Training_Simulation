using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Removes dart from scene after a certain amount of time to save on draw calls.
 */
public class TidyDart : MonoBehaviour
{
    private float removalTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, removalTime); //destroy game object after 1 second to save batch calls
    }   


}
