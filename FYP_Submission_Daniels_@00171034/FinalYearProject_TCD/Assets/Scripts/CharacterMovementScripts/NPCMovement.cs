using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/**
 * Abstract class to handle the navmesh agent traversal of nodes. This can be subclassed by a script for each NPC in the
 * scene to enable them to override certain methods to add functionality specific to that NPC object.
 */
public abstract class NPCMovement : MonoBehaviour
{
    protected NavMeshAgent navAgent;
    protected Animator animatior;
    protected int destination;
    [SerializeReference] protected GameObject[] nodes; //SerializeReference allows field from abstract class to be viewable in inspector

    [SerializeReference] protected bool move;
    [SerializeReference] protected bool beginMovement;

    // Start is called before the first frame update
    void Awake()
    {
        animatior = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        destination = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Handles movement to each node after the first node. virtual prefix allows this to be overriden.
     */
    protected virtual void MoveToPosition()
    {
        if (!navAgent.pathPending)
        {
            if (navAgent.remainingDistance <= 0.5f && destination < nodes.Length)
            {
                if (destination == nodes.Length - 1)
                {
                    navAgent.destination = nodes[destination].transform.position;
                    destination++;
                }
                else
                {
                    destination++;
                    navAgent.destination = nodes[destination].transform.position;
                }
            }
        }
    }

    /**
     * Begind initial movement to first node. This can be overriden if required
     */
    public virtual void StartMovement()
    {
        if (nodes.Length == 0) //Check the array is populated
            return;

        animatior.SetBool("Walking", true);
        navAgent.destination = nodes[destination].transform.position;
        move = true;
        beginMovement = false;
    }

}
