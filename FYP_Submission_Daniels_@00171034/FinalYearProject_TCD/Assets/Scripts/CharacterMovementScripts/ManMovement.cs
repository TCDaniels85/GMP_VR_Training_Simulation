using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

/**
 * Class which handles scene specific movement for the male character. Subclasses the NPCMovement script and inherits methods and fields
 */
public class ManMovement : NPCMovement
{
    public float rotationSpeed = 50;    
    private bool sit;   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // conditional statements called in the update method to call relevant movement methods
    void Update()
    {

        if (beginMovement)
        {
            StartMovement();
        }
        if (move)
        {
            MoveToPosition();
        }
        if (sit)
            Sit();
        
    }

    /**
     * Override Method to add additional funcationality specific to this scene.
     */
    protected override void MoveToPosition()
    {
        base.MoveToPosition();        
        if (destination == nodes.Length)
        {
            navAgent.isStopped = true;
            animatior.SetBool("Walking", false);
            move = false;
            sit = true;
        }
    }

    
    /**
     * Handles character sitting animation and position
     */
    private void Sit()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 270, 0);
        if (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animatior.SetTrigger("Sit");
        }

        if (transform.position.x < 35.831)
        {
            gameObject.transform.Translate(Vector3.back * Time.deltaTime);
        }
        //navAgent.baseOffset = 0.63f; 
        navAgent.baseOffset = 0.43f; // offset changed to suit character model

    }

}
