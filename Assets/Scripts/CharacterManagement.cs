using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///Responsible for adding and maintaining characters in the scene.
///</summary>
public class CharacterManagement : MonoBehaviour
{

    public static CharacterManagement Instance;

    ///<summary>
    ///All the characters should be attched to the character panel
    ///</summary>
    public RectTransform CharacterPanel;


    ///<summary>
    ///Holds all the characters currently in the scene
    ///</summary>
    public List<Character> Characters = new List<Character>();

    ///<summary>
    ///string = name of the character, int = index in characters list
    ///</summary>
    public Dictionary<string, int> CharactersDictionary = new Dictionary<string, int>();

    private void Awake()
    {
        Instance = this;
    }

    ///<summary>
    ///Return a character with the name from characters list, if found
    ///</summary>
    ///<param name="name">Character's name.</param>
    ///<param name="createCharacterIfNotFound">Create a new character if a character with input name wasn't found.</param>
    ///<param name="enableCreatedCharacterOnStart">Should the character be visible right at the start?</param>
    public Character GetCharacter(string name, bool createCharacterIfNotFound = true, bool enableCreatedCharacterOnStart = true)
    {
        //placeholder value
        int index = -1;
        if (CharactersDictionary.TryGetValue(name, out index))
        {//character exists
            return Characters[index];
        }
        else if (createCharacterIfNotFound)
        {
           return CreateCharacter(name, enableCreatedCharacterOnStart);
        }
        return null;
    }

    ///<summary>
    ///Create a new character with given name
    ///</summary>
    ///<param name="name">Character's name.</param>
    ///<param name="enableCharacterOnStart">Should the character be visible right at the start?</param>
    public Character CreateCharacter(string name, bool enableCharacterOnStart = true)
    {
        //make new character
        Character newCharacter = new Character(name, enableCharacterOnStart);

        //add to dictionary, index should be the current count of the list
        CharactersDictionary.Add(name, Characters.Count);

        //add the new characters to the characters list
        Characters.Add(newCharacter);

        return newCharacter;
    }

}
