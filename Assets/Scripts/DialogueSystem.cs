using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;
    public ELEMENTS elements;

    [HideInInspector] public bool waitingForUserInput = false;

    Coroutine speaking = null;
    public bool isSpeaking { get { return speaking != null; } }
    public string targetSpeech = "";

    [System.Serializable]
    public class ELEMENTS
    {
        ///<summary>
        ///The main panel containing all dialogue related elements on the ui
        ///</summary>
        public GameObject speechPanel;
        public TMP_Text speakerNameText;
        public TMP_Text dialogueText;
    }

    public GameObject SpeechPanel { get { return elements.speechPanel; } }
    public TMP_Text SpeakerNameText { get { return elements.speakerNameText; } }
    public TMP_Text DialogueText { get { return elements.dialogueText; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }

    ///<summary>
    ///override previous text and show it on the speech box
    ///</summary>
    public void Say(string speech, string speaker = "")
    {
        StopSpeaking();
        DialogueText.text = targetSpeech;//when dialogue is skipped show the target text fully
        speaking = StartCoroutine(Speaking(speech, false, speaker));
    }

    ///<summary>
    ///add new text to previous text and show it on the speech box
    ///</summary>
    public void SayAdditive(string speech, string speaker = "")
    {
        StopSpeaking();
        DialogueText.text = targetSpeech;//when dialogue is skipped show the target text fully
        speaking = StartCoroutine(Speaking(speech, true, speaker));
    }

    IEnumerator Speaking(string speech, bool additive, string speaker = "")
    {
        SpeechPanel.SetActive(true);//make speech panel visible and active
        SpeakerNameText.text = determineSpeaker(speaker);//temporary
        targetSpeech = speech;

        //check if text is additive or overriding
        if (!additive)
        {
            DialogueText.text = "";
        }
        else
        {
            targetSpeech = DialogueText.text + targetSpeech;
        }

        //reset at the start of new text
        waitingForUserInput = false;

        //while the text in the dialogue box isn't the finished text add text
        while (DialogueText.text != targetSpeech)
        {
            //take the next letter from target speech by counting how many letters have been added to dialogue text already
            DialogueText.text += targetSpeech[DialogueText.text.Length];
            yield return new WaitForEndOfFrame(); //wait until end of frame
        }

        //once text is finished wait for user to continue
        waitingForUserInput = true;

        while (waitingForUserInput)
        {
            yield return new WaitForEndOfFrame();
        }
        StopSpeaking();
    }

    public void StopSpeaking()
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
        }
        speaking = null;
    }

    ///<summary>
    ///if no new name is given, will return the last known
    ///</summary>
    string determineSpeaker(string s)
    {
        string val = SpeakerNameText.text;//default return value is the current value
        if (s != SpeakerNameText.text && s != "")
        {
            val = (s.ToLower().Contains("narrator")) ? "" : s;
        }
        return val;
    }

    ///<summary>
    ///stop all speech and close dialogue panel
    ///</summary>
    public void Close()
    {
        StopSpeaking();
        SpeechPanel.SetActive(false);
    }
}
