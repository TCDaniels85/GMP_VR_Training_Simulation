using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

/**
 * Class that uses IPonterHadler interfaces to enable text to be displayed dynamically when UI object is highlighted
 */
public class MenuEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject informationText;
    private TextMeshProUGUI infoText;

    void Start()
    {
        infoText = informationText.GetComponent<TextMeshProUGUI>();
    }
    
    
    /**
     * Sets on menu hint text dependant on item that is being hovered over
     * @param item ray is currently pointing at
     */
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.name == "StartButton")
        {
            infoText.text = "Start the simulation from the beginning and make your way through each scenario in order.";
        }
        if (this.name == "SelectSceneButton")
        {
            infoText.text = "Restart the tutorial to become more accustomed to the controls.";
        }
        if (this.name == "SettingsButton")
        {
            infoText.text = "Adjust some of settings of the experience.";
        }
        if (this.name == "QuitButton")
        {
            infoText.text = "Exit the application and return to the head set home screen.";
        }        
        if (this.name == "ReturnButton")
        {
            infoText.text = "Return to previous menu.";
        }
        if(this.name == "SubtitleToggle")
        {
            infoText.text = "Add subtitles to the dialogue in the scene.";
        }
        if (this.name == "SnapTurnToggle ")
        {
            infoText.text = "Use the snap turn function. When using the controller to turn, your character will turn in 45 degree increments.";
        }
        if (this.name == "ConTurnToggle ")
        {
            infoText.text = "Use the continuous turn function. When using the controller to turn, your character will turn in a continuous smooth motion.";
        }



    }

    /**
     * Resets text to nothing
     */
    public void OnPointerExit(PointerEventData eventData)
    {
        infoText.text = "";
    }

       
}
