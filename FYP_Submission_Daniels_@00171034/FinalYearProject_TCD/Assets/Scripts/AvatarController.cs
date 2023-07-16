using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A class to map the vr target (eg hand controllers or headset position) to the inverse kinematics target on the 
 * animation rigging skeleton. Allows a positional and rotational offset to be used to enure correct positioning.
 */
[System.Serializable]
public class MapAvatarTransforms
{
    public Transform vrTarg;
    public Transform ikTarg;
    //Allows offsets to be created to ensure skeleton position matches player position.
    public Vector3 positionOffset; 
    public Vector3 rotationalOffset;

    /**
     * Maps the IK target to the VR component using the offsets, 
     */
    public void Mapping()
    {
        ikTarg.position = vrTarg.TransformPoint(positionOffset); //transform from local space to world space
        ikTarg.rotation = vrTarg.rotation * Quaternion.Euler(rotationalOffset);
        //
    }
}

/**
 *Class to enable mapping to be performed on each VR component (in this case hand controllers and headset)
 */
public class AvatarController : MonoBehaviour
{
    [SerializeField] private MapAvatarTransforms head;
    [SerializeField] private MapAvatarTransforms rHand;
    [SerializeField] private MapAvatarTransforms lHand;
    [SerializeField] private float smoothTurning;  //Float to smooth turning
    [SerializeField] Transform headIK;
    [SerializeField] Vector3 headOffsetToBody; //To create an offset position from the body, for the head.

    private void LateUpdate()
    {
        transform.position = headIK.position + headOffsetToBody;
        //smooths horizontal motion, ignoring the vertical orientation
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headIK.forward, Vector3.up).normalized, Time.deltaTime * smoothTurning);   
        //call the maaping method on each component
        head.Mapping();
        lHand.Mapping();
        rHand.Mapping();
    }



}
