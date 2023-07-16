using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * Class which handles scene specific movement for the female character. Subclasses the NPCMovement script and inherits methods and fields
 */
public class WomanMovement : NPCMovement
{    
    public float rotationSpeed = 100;   
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
     * Handles movement to each node after the first node.
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
     * Handles sitting animation for character
     */
    private void Sit()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
        if (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        } else
        {
            animatior.SetTrigger("Sit");
        }

        if (transform.position.z > -3.66f)
        {
            gameObject.transform.Translate(Vector3.back * Time.deltaTime);
        }
        
    }

    /**
     * Sets beginMovement variable to true
     */
    public void BeginMovement()
    {
        beginMovement = true;
    }


}
