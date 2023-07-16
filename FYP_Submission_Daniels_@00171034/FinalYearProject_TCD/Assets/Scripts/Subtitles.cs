using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * Class to handle the display of subtitles
 */
public class Subtitles : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private GameObject subtitleUI;
    [SerializeField] private TextAsset conversationOneText;
    [SerializeField] private TextAsset conversationTwoText;
    [SerializeField] private TextAsset policeCallText;
    [SerializeField] private TextAsset insuranceCallText;
    [SerializeField] private TextAsset singleLineText;
    [SerializeField] private TextAsset narrationText;

    private Dictionary<string, string> singleDialogueLines;
    private Dictionary<string, string[]> conversationDialogue;
    private string[] conversationOne = new string[6];
    private string[] conversationTwo = new string[4];
    private string[] policeCall = new string[8];
    private string[] insuranceCall = new string[8];
    private string[] singleDialogueLine = new string[8];
    private string[] narration = new string[6];

    // Start is called before the first frame update

    void Start()
    {
        singleDialogueLines = new Dictionary<string, string>();
        conversationDialogue = new Dictionary<string, string[]>();

        PopulateArrays();

    }

    /**
     * Tests that subtitles can be shown then displays the relevent dialogue text from the array
     * @param dialogue line text Key Value
     */
    public void DisplaySubtitle(string dialogueLine)
    {        
        if (SubtitlesActivated())
        {
            subtitleUI.SetActive(true);
            subtitleText.text = singleDialogueLines[dialogueLine];
        }
        
    }

    /**
     * Displays subtitles from the conversation array
     * @param dialogue line text Key Value
     * @param index of the line to be called.
     */
    public void DisplaySubtitleArray(string dialogueArray, int index)
    {
        if (SubtitlesActivated())
        {
            subtitleUI.SetActive(true);
            string[] array = conversationDialogue[dialogueArray];//retrieves array from dictionary
            subtitleText.text = array[index]; //retrieves string from array
        }        
    }
    
    /**
     * Hides subtitle UI by deactivating it.
     */
    public void HideSubtitle()
    {
        subtitleUI.SetActive(false);
    }

    //Used In testing to test array population.
    //private void TestArray(string[] conversation)
    //{
    //    foreach(string a in conversation)
    //    {
    //        Debug.Log(a);
    //    }
    //}

    /**
     * Checks playerprefs as to whether or not subtitles are activated.
     * @return activation status
     */
    private bool SubtitlesActivated()
    {
        
        if(PlayerPrefs.GetInt("Subtitles") == 1)        
        {
            return true;
        } else
        {
            return false;
        }
    }

    /**
     * Populates the arrays and dictionarys with text from the relvant text file.
     */
    private void PopulateArrays()
    {
        singleDialogueLine = singleLineText.text.Split("\n"[0]);
        singleDialogueLines.Add("WomanDialogue1", singleDialogueLine[0]);
        singleDialogueLines.Add("f-dialogue2", singleDialogueLine[1]);
        singleDialogueLines.Add("f-dialogue3", singleDialogueLine[2]);
        singleDialogueLines.Add("f-dialogue4", singleDialogueLine[3]);
        singleDialogueLines.Add("m-dialogue1", singleDialogueLine[4]);
        singleDialogueLines.Add("m-dialogue2", singleDialogueLine[5]);
        singleDialogueLines.Add("InternalDialogue1", singleDialogueLine[6]);
        singleDialogueLines.Add("InternalDialogue2", singleDialogueLine[7]);


        conversationOne = conversationOneText.text.Split("\n"[0]); //splits the text into strings every newline, then adds them to an array
        conversationTwo = conversationTwoText.text.Split("\n"[0]);
        policeCall = policeCallText.text.Split("\n"[0]);
        insuranceCall = insuranceCallText.text.Split("\n"[0]);
        narration = narrationText.text.Split("\n"[0]);
        conversationDialogue.Add("userParentDialogue1", conversationOne);
        conversationDialogue.Add("maleCinemaDialogue", conversationTwo);
        conversationDialogue.Add("policeUserDialogue1", policeCall);
        conversationDialogue.Add("bankUserPhoneDialogue", insuranceCall);
        conversationDialogue.Add("bathroomNarration", narration);
    }
}
