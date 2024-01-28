using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
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

    [SerializeField]
    Image SubjectImage;

    [SerializeField]
    Image VerbImage;

    [SerializeField]
    Image ComplementImage;

    [SerializeField]
    TMP_Text LevelText;

    [SerializeField]
    TMP_Text RoundText;

    //[SerializeField]
    //Animator PanelAnimator;

    private void Start()
    {

    }

    public void InitializeOptions(int level)
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

        SubjectSelected(subjectSelector);
        ObjectSelected(objectSelector);

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

        VerbSelected(verbSelector);

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

        AdjectiveSelected(adjectiveSelector);
        InitializeRound(1);
        LevelText.text = $"Level: {level.ToString()}";

        //PanelAnimator.SetBool("DayStart", true);
        //PanelAnimator.SetBool("DayEnd", false);
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

        SetSelectedSubject(selectedSubject);
    }
    void VerbSelected(TMP_Dropdown verbSelector)
    {
        selectedVerb = (JokeManager.Verb)Enum.Parse(typeof(JokeManager.Verb), verbSelector.options[verbSelector.value].text);
        SetSelectedVerb(selectedVerb);
    }

    void ObjectSelected(TMP_Dropdown objectSelector)
    {
        selectedObject = (JokeManager.Noun)Enum.Parse(typeof(JokeManager.Noun), objectSelector.options[objectSelector.value].text);

        SetSelectedObject(selectedObject);
    }

    void AdjectiveSelected(TMP_Dropdown adjectiveSelector)
    {
        selectedAdjective = (JokeManager.Adjective)Enum.Parse(typeof(JokeManager.Adjective), adjectiveSelector.options[adjectiveSelector.value].text);

        SetSelectedAdjective(selectedAdjective);
    }

    public void ToggleVisibility()
    {
        panel.SetActive(!panel.activeInHierarchy);
        ShowHideText.text = panel.activeInHierarchy ? "/\\" : "\\/";
    }

    public void InitializeRound(int round)
    {
        RoundText.text = $"Round: {round.ToString()}";

        verbSelector.gameObject.SetActive(false);
        objectSelector.gameObject.SetActive(false);
        adjectiveSelector.gameObject.SetActive(false);

        ResetSubject();
        ResetVerb();
        ResetAdjective();
        ResetObject();

        submitButton.enabled = false;
    }

    private void ResetObject()
    {
        objectSelector.value = 0;
        SetSelectedObject(JokeManager.Noun.None);
    }

    private void ResetAdjective()
    {
        adjectiveSelector.value = 0;
        SetSelectedAdjective(JokeManager.Adjective.None);
    }

    private void ResetVerb()
    {
        verbSelector.value = 0;
        SetSelectedVerb(JokeManager.Verb.None);
    }

    private void ResetSubject()
    {
        subjectSelector.value = 0;
        SetSelectedSubject(JokeManager.Noun.None);
    }

    private void SetSelectedObject(JokeManager.Noun noun)
    {
        ComplementImage.sprite = noun != JokeManager.Noun.None ? objectSelector.options[objectSelector.value].image : null;

        submitButton.enabled = true;
    }

    private void SetSelectedAdjective(JokeManager.Adjective adjective)
    {
        ComplementImage.sprite = adjective != JokeManager.Adjective.None ? adjectiveSelector.options[adjectiveSelector.value].image : null;
        submitButton.enabled = true;
    }

    private void SetSelectedSubject(JokeManager.Noun noun)
    {
        SubjectImage.sprite = null;

        if (noun != JokeManager.Noun.None)
        {
            SubjectImage.sprite = subjectSelector.options[subjectSelector.value].image;
            verbSelector.gameObject.SetActive(true);
        }
    }

    private void SetSelectedVerb(JokeManager.Verb verb)
    {
        if (verb == JokeManager.Verb.None)
        {
            VerbImage.sprite = null;
            objectSelector.gameObject.SetActive(false);
            adjectiveSelector.gameObject.SetActive(false);
        }
        else
        {
            if (verb == JokeManager.Verb.Is)
            {
                selectedStructure = JokeManager.JokeStructure.SubjectIsAdjective;
                adjectiveSelector.gameObject.SetActive(true);
                objectSelector.gameObject.SetActive(false);
            }
            else
            {
                selectedStructure = JokeManager.JokeStructure.SubjectVerbObject;
                objectSelector.gameObject.SetActive(true);
                adjectiveSelector.gameObject.SetActive(false);
            }

            VerbImage.sprite = verbSelector.options[verbSelector.value].image;
        }
        submitButton.enabled = false;
    }

    public void OnPanelIn()
    {
        Debug.Log("X");
    }

    internal void OnPanelOut()
    {
        Debug.Log("Y");
    }
}
