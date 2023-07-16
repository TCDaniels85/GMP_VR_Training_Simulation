using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Class to handle creation of the in game menu
 */
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputAction pauseButton;
    [SerializeField] private GameObject pauseMenuCanvas;
    private Vector3 headPosition;
    private Vector3 viewDirection;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (pauseButton.WasPressedThisFrame())
        {
            Debug.Log("pressed");
            if (isPaused)
            {
                ResumeSim();
            } else
            {
                PauseSim();
            }
        }
    }

    /**
     * Brings up menu to enable the user to select options. This uses the users camera position and gaze direction
     * to place a world space menu containing the in-game options. A raycast is used to check for objects to ensure
     * the menu does not appear behing a wall.
     */
    void PauseSim()
    {
        headPosition = Camera.main.transform.position;        
        viewDirection = Camera.main.transform.forward;
        viewDirection.y = 0.2f; //offset the menu, this is above most in scene objects
        Vector3 menuRotation = Camera.main.transform.eulerAngles;
        pauseMenuCanvas.transform.eulerAngles = menuRotation;
        //disable unwanted x and z axis rotation
        menuRotation.z = 0;
        menuRotation.x = 0;        
        Vector3 menuPosition = headPosition + viewDirection * 3.0f;
        //Raycast to check for objects that may obstruct view of UI
        RaycastHit hit;
        if (Physics.Raycast(headPosition, viewDirection, out hit, 3.0f, -5, QueryTriggerInteraction.Ignore)) //ignore trggercolliders
        {            
            menuPosition = hit.point - viewDirection.normalized * 0.1f; //offset from wall surface
        }
            
        //Calculates the closest 90 degree rotation, prevents menu clipping through walls at an angle
        float nearestNinety = Mathf.Round(menuRotation.y / 90.0f) * 90.0f; 
        menuRotation.y = nearestNinety;
        pauseMenuCanvas.transform.eulerAngles = menuRotation;        
        pauseMenuCanvas.transform.position = menuPosition;        
        pauseMenuCanvas.SetActive(true);        
        isPaused = true;
    }

    /**
     * Remove menu canvas from scene
     */
    public void ResumeSim()
    {
        pauseMenuCanvas.SetActive(false);       
        isPaused = false;
    }

    /**
     * Quit scene and return to main menu scene
     */
    public void QuitScene()
    {
        SceneLoader.LoadMyScene(0);
    }

    //Methods related to the InputAction
    private void OnEnable()
    {
        pauseButton.Enable();
    }

    private void OnDisable()
    {
        pauseButton.Disable();
    }
}
