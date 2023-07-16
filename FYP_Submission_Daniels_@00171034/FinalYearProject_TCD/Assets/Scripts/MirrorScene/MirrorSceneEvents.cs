using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorSceneEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerScript.timer > 29)
        {
            SceneLoader.LoadMyScene(2);
        }
        
    }
}
