using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character : MonoBehaviour
{
    public string CharacterName;

    public bool Enabled { get { return root.gameObject.activeInHierarchy; } set { root.gameObject.SetActive(value); } }

    ///<summary>
    ///The root of all images related to the character in the scene.
    ///</summary>
    [HideInInspector] public RectTransform root;

    //returns null if the character is a single image, since rendrers.renderer didn't have anything to return
    public bool IsMultiLayerCharacter { get { return renderers.renderer == null; } }

    public Renderers renderers = new Renderers();

    private DialogueSystem dialogue;

    ///<summary>
    ///Create anew character
    ///</summary>
    ///<param name="name">Character's name.</param>
    public Character(string name, bool enableOnStart = true)
    {
        CharacterManagement cm = CharacterManagement.Instance;
        GameObject prefab = (GameObject)Resources.Load($"Characters/Character[{name}]");//get character prefab
        GameObject ob = GameObject.Instantiate(prefab, cm.CharacterPanel);//instantiate character under character panel

        root = ob.GetComponent<RectTransform>();
        CharacterName = name;

        //get all the renderers
        renderers.renderer = ob.GetComponentInChildren<RawImage>();
        if (IsMultiLayerCharacter)
        {
            renderers.bodyRenderer = ob.transform.Find("Body").GetComponent<Image>();
            renderers.expressionRenderer = ob.transform.Find("Expression").GetComponent<Image>();
        }
        dialogue = DialogueSystem.Instance;
        Enabled = enableOnStart;
    }

    ///<summary>
    ///Makes the character say something
    ///</summary>
    ///<param name="speech">What the chaarcter will say.</param>
    ///<param name="add">If true the speech will use additive speech other wise it will use overriding speech</param>
    public void Say(string speech, bool add = false)
    {
        if (!Enabled)
        {
            Enabled = true;
        }
        if (!add)
        {
            dialogue.Say(speech, CharacterName);
        }
        else
        {
            dialogue.SayAdditive(speech, CharacterName);
        }
    }

    [System.Serializable]
    public class Renderers
    {

        ///<summary>
        ///Image used for a character, when it's made up of a single layer
        ///</summary>
        public RawImage renderer;

        ///<summary>
        ///Image used as a base or or body when character is made of multiple images
        ///</summary>
        public Image bodyRenderer;

        ///<summary>
        ///Image used for the expression when character is made of multiple images
        ///</summary>
        public Image expressionRenderer;

    }
}
