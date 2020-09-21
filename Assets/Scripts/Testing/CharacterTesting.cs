using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTesting : MonoBehaviour
{
    public Character Gilbert;

    int index = 0;
    public string[] s = new string[]{
        "Oh hey, it's you...",
        "I am so awesome!",
        "You wouldn't believe it anyways."
    };


    // Start is called before the first frame update
    void Start()
    {
        Gilbert = CharacterManagement.Instance.GetCharacter("Gilbert", enableCreatedCharacterOnStart: false);
        if (!Gilbert)
        {
            Debug.LogWarning($"{Gilbert} wasn't found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            nextText();
        }
    }

    public void nextText()
    {
        if (index < s.Length)
        {
            Gilbert.Say(s[index]);
            index++;
        }
        else
        {
            DialogueSystem.Instance.Close();
            index = 0;
        }
    }
}
