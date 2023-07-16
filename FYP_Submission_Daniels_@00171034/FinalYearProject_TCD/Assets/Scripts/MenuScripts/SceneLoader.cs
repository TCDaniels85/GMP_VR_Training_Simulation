using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Script to handle scene loading and ending the application
 */
public class SceneLoader : MonoBehaviour
{
        

    /**
     * Loads desired scene according to scene index, static method can be accessed without need for
     * object reference.
     * @param index of scene to be loaded
     */
    public static void LoadMyScene(int sceneIndex)
    {
        
        SceneManager.LoadScene(sceneIndex);
        
    }    
    

    /**
     * Ends application and returns to oculus home screen
     */
    public void EndApplication()
    {
        Application.Quit();  
        

    }
}
