using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    JokeManager jokeManager;

    [SerializeField]
    Sprite sprite;
    
    private void Start()
    {
        subjectSelector.ClearOptions();
        subjectSelector.AddOptions(
            new List<TMP_Dropdown.OptionData>()
            { 
                new TMP_Dropdown.OptionData("King", sprite), 
                new TMP_Dropdown.OptionData("Queen", sprite)
            }
        );

        subjectSelector.onValueChanged.AddListener(delegate { SubjectSelected(subjectSelector); });

        verbSelector.ClearOptions();
        verbSelector.AddOptions(
            new List<TMP_Dropdown.OptionData>()
            {
                new TMP_Dropdown.OptionData("Is", sprite),
                new TMP_Dropdown.OptionData("Eats", sprite),
                new TMP_Dropdown.OptionData("Farts", sprite),
                new TMP_Dropdown.OptionData("Kills", sprite)
            }
        );

        verbSelector.onValueChanged.AddListener(delegate { VerbSelected(verbSelector); });

        objectSelector.ClearOptions();
        objectSelector.AddOptions(
            new List<TMP_Dropdown.OptionData>()
            {
                new TMP_Dropdown.OptionData("King", sprite),
                new TMP_Dropdown.OptionData("Queen", sprite)
            }
        );

        objectSelector.onValueChanged.AddListener(delegate { ObjectSelected(objectSelector); });

        adjectiveSelector.ClearOptions();
        adjectiveSelector.AddOptions(
            new List<TMP_Dropdown.OptionData>()
            {
                new TMP_Dropdown.OptionData("Fat", sprite),
                new TMP_Dropdown.OptionData("Ugly", sprite),
                new TMP_Dropdown.OptionData("Poor", sprite),
                new TMP_Dropdown.OptionData("Vain", sprite),
            }
        );

        adjectiveSelector.onValueChanged.AddListener(delegate { AdjectiveSelected(adjectiveSelector); });
    }

    JokeManager.JokeStructure selectedStructure;
    [SerializeField]
    TMP_Dropdown subjectSelector;
    JokeManager.Noun selectedSubject;
    [SerializeField]
    TMP_Dropdown verbSelector;
    JokeManager.Verb selectedVerb;
    [SerializeField]
    TMP_Dropdown objectSelector;
    JokeManager.Noun selectedObject;
    [SerializeField]
    TMP_Dropdown adjectiveSelector;
    JokeManager.Adjective selectedAdjective;
    [SerializeField]
    Button submitButton;

    public void SubmitJoke()
    {
        if (selectedStructure == JokeManager.JokeStructure.SubjectVerbObject)
            jokeManager.SubmitJoke(selectedSubject, selectedVerb, selectedObject);
        else
            jokeManager.SubmitJoke(selectedSubject, selectedAdjective);
    }


    void SubjectSelected(TMP_Dropdown subjectSelector)
    {
        selectedSubject = (JokeManager.Noun)Enum.Parse( typeof(JokeManager.Noun), subjectSelector.options[subjectSelector.value].text);
    }
    void VerbSelected(TMP_Dropdown verbSelector)
    {
        selectedVerb = (JokeManager.Verb)Enum.Parse(typeof(JokeManager.Verb), verbSelector.options[verbSelector.value].text);
        if (selectedVerb == JokeManager.Verb.Is)
            selectedStructure = JokeManager.JokeStructure.SubjectIsAdjective;
        else
            selectedStructure = JokeManager.JokeStructure.SubjectVerbObject;
    }

    void ObjectSelected(TMP_Dropdown objectSelector)
    {
        selectedObject = (JokeManager.Noun)Enum.Parse(typeof(JokeManager.Noun), objectSelector.options[objectSelector.value].text);
    }

    void AdjectiveSelected(TMP_Dropdown adjectiveSelector)
    {
        selectedAdjective = (JokeManager.Adjective)Enum.Parse(typeof(JokeManager.Adjective), adjectiveSelector.options[adjectiveSelector.value].text);
    }

}
