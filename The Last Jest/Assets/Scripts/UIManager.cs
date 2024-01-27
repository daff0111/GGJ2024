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
    IconContainer iconContainer;

    [SerializeField]
    JokeManager jokeManager;

    [SerializeField]
    Sprite sprite;

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
    [SerializeField]
    GameObject panel;
    [SerializeField]
    TMP_Text ShowHideText;


    private void Start()
    {
        InitializeOptions(jokeManager.Level);
    }

    private void InitializeOptions(int level)
    {
        List<TMP_Dropdown.OptionData> subjectOptions = new List<TMP_Dropdown.OptionData>();
        List<TMP_Dropdown.OptionData> objectOptions = new List<TMP_Dropdown.OptionData>();
        foreach (var nounSpritePair in iconContainer.NounSprites)
        {
            if (nounSpritePair.UnlockLevel <= level)
            {
                subjectOptions.Add(new TMP_Dropdown.OptionData(nounSpritePair.Noun.ToString(), nounSpritePair.Sprite));
                objectOptions.Add(new TMP_Dropdown.OptionData(nounSpritePair.Noun.ToString(), nounSpritePair.Sprite));
            }
        }
        subjectSelector.ClearOptions();
        subjectSelector.AddOptions(subjectOptions);

        objectSelector.ClearOptions();
        objectSelector.AddOptions(objectOptions);

        subjectSelector.onValueChanged.AddListener(delegate { SubjectSelected(subjectSelector); });
        objectSelector.onValueChanged.AddListener(delegate { ObjectSelected(objectSelector); });

        List<TMP_Dropdown.OptionData> verbOptions = new List<TMP_Dropdown.OptionData>();
        foreach (var verbSpritePair in iconContainer.VerbSprites)
        {
            if (verbSpritePair.UnlockLevel <= level)
            {
                verbOptions.Add(new TMP_Dropdown.OptionData(verbSpritePair.Verb.ToString(), verbSpritePair.Sprite));
            }
        }
        verbSelector.ClearOptions();
        verbSelector.AddOptions(verbOptions);
        verbSelector.onValueChanged.AddListener(delegate { VerbSelected(verbSelector); });


        List<TMP_Dropdown.OptionData> adjectiveOptions = new List<TMP_Dropdown.OptionData>();
        foreach (var adjectiveSpritePair in iconContainer.AdjectiveSprites)
        {
            if (adjectiveSpritePair.UnlockLevel <= level)
            {
                adjectiveOptions.Add(new TMP_Dropdown.OptionData(adjectiveSpritePair.Adejctive.ToString(), adjectiveSpritePair.Sprite));
            }
        }
        adjectiveSelector.ClearOptions();
        adjectiveSelector.AddOptions(adjectiveOptions);
        adjectiveSelector.onValueChanged.AddListener(delegate { AdjectiveSelected(adjectiveSelector); });
    }

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

    public void ToggleVisibility()
    {
        panel.SetActive(!panel.activeInHierarchy);
        ShowHideText.text = panel.activeInHierarchy ? "/\\" : "\\/";
    }
}
