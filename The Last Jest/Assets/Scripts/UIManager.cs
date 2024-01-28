using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static JokeManager;

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

    [SerializeField]
    Animator CardAnimator;

    [SerializeField]
    float LevelBeginDelay = 2.5f;

    [SerializeField]
    float RoundBeginDelay = 2.5f;

    [SerializeField]
    float SubmissionDelay = 2.5f;

    [SerializeField]
    float gameOverDelay = 2.5f;

    [SerializeField]
    TMP_Text announcementText;
    [SerializeField]
    Image announcementBack;

    [SerializeField]
    Sprite cardBack;

    [SerializeField]
    GameObject gameOverPanel;
    [SerializeField]
    TMP_Text gameOverText;
    [SerializeField]
    TMP_Text jokeCountText;
    [SerializeField]
    Button playAgain;

    
    [SerializeField]
    StudioEventEmitter pigEvent;
    [SerializeField]
    StudioEventEmitter foodEvent;
    [SerializeField]
    StudioEventEmitter smallEvent;
    [SerializeField]
    StudioEventEmitter isEvent;
    [SerializeField]
    StudioEventEmitter fartsEvent;
    [SerializeField]
    StudioEventEmitter hatesEvent;
    [SerializeField]
    StudioEventEmitter killsEvent;
    [SerializeField]
    StudioEventEmitter lovesEvent;
    [SerializeField]
    StudioEventEmitter servesEvent;
    private void Start()
    {

    }

    public void StartLevel(int level)
    {
        StartCoroutine(StartLevelDelayed(level));
    }

    IEnumerator StartLevelDelayed(int level)
    {
        panel.SetActive(false);
        announcementBack.gameObject.SetActive(true);
        announcementText.text = $"Day {level}";

        yield return new WaitForSeconds(LevelBeginDelay);

        announcementBack.gameObject.SetActive(false);

        panel.SetActive(true);
        InitializeOptions(level);
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
        
        subjectSelector.gameObject.SetActive(false);
        verbSelector.gameObject.SetActive(false);
        objectSelector.gameObject.SetActive(false);
        adjectiveSelector.gameObject.SetActive(false);

        InitializeRound(1);
        LevelText.text = $"Level: {level.ToString()}";
        RoundText.text = $"Round: ";

    }

    public void SubmitJoke()
    {
        subjectSelector.enabled = false;
        objectSelector.enabled = false;
        verbSelector.enabled = false;
        adjectiveSelector.enabled = false;
        submitButton.enabled = false;

        StartCoroutine(SubmitJokeDelayed());
    }

    IEnumerator SubmitJokeDelayed()
    {
        CardAnimator.SetTrigger("Submit");

        yield return new WaitForSeconds(SubmissionDelay);

        if (selectedStructure == JokeManager.JokeStructure.SubjectVerbObject)
            jokeManager.SubmitJoke(selectedSubject, selectedVerb, selectedObject);
        else
            jokeManager.SubmitJoke(selectedSubject, selectedAdjective);

        subjectSelector.enabled = true;
    }

    void SubjectSelected(TMP_Dropdown subjectSelector)
    {
        selectedSubject = (JokeManager.Noun)Enum.Parse( typeof(JokeManager.Noun), subjectSelector.options[subjectSelector.value].text);

        SetSelectedSubject(selectedSubject);

        verbSelector.enabled = true;
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
        StartCoroutine(StartRoundDelayed(round));
    }

    IEnumerator StartRoundDelayed(int round)
    {
        submitButton.enabled = false;

        if (round != 1)
            yield return new WaitForSeconds(RoundBeginDelay);

        RoundText.text = $"Round: {round.ToString()}";

        subjectSelector.gameObject.SetActive(true);
        verbSelector.gameObject.SetActive(false);
        objectSelector.gameObject.SetActive(false);
        adjectiveSelector.gameObject.SetActive(false);

        ResetSubject();
        ResetVerb();
        ResetAdjective();
        ResetObject();

        yield return null;
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
        ComplementImage.sprite = noun != JokeManager.Noun.None ? objectSelector.options[objectSelector.value].image : cardBack;

        PlayNounAudio(noun);

        submitButton.enabled = true;
    }

    private void PlayNounAudio(JokeManager.Noun noun)
    {
        switch (noun)
        {
            case JokeManager.Noun.Food:
                foodEvent.Play();
                break;
        }
    }

    private void SetSelectedAdjective(JokeManager.Adjective adjective)
    {
        ComplementImage.sprite = adjective != JokeManager.Adjective.None ? adjectiveSelector.options[adjectiveSelector.value].image : cardBack;

        switch(adjective)
        {
            case JokeManager.Adjective.Pig:
                pigEvent.Play();
                break;
            case JokeManager.Adjective.Small:
                smallEvent.Play();
                break;
        }    

        submitButton.enabled = true;
    }

    private void SetSelectedSubject(JokeManager.Noun noun)
    {
        SubjectImage.sprite = cardBack;

        if (noun != JokeManager.Noun.None)
        {
            SubjectImage.sprite = subjectSelector.options[subjectSelector.value].image;
            PlayNounAudio(noun);
            verbSelector.gameObject.SetActive(true);
        }
    }

    private void SetSelectedVerb(JokeManager.Verb verb)
    {
        if (verb == JokeManager.Verb.None)
        {
            VerbImage.sprite = cardBack;
            objectSelector.gameObject.SetActive(false);
            adjectiveSelector.gameObject.SetActive(false);
        }
        else
        {
            if (verb == JokeManager.Verb.Is)
            {
                selectedStructure = JokeManager.JokeStructure.SubjectIsAdjective;
                adjectiveSelector.gameObject.SetActive(true);
                adjectiveSelector.enabled = true;
                objectSelector.gameObject.SetActive(false);
                isEvent.Play();
            }
            else
            {
                selectedStructure = JokeManager.JokeStructure.SubjectVerbObject;
                objectSelector.gameObject.SetActive(true);
                objectSelector.enabled = true;
                adjectiveSelector.gameObject.SetActive(false);
                PlayVerbAudio(verb);
            }

            VerbImage.sprite = verbSelector.options[verbSelector.value].image;
        }
        submitButton.enabled = false;
    }

    private void PlayVerbAudio(Verb verb)
    {
        switch(verb)
        {
            case Verb.Farts:
                fartsEvent.Play();
                break;
            case Verb.Hates:
                hatesEvent.Play();
                break; 
            case Verb.Kills: killsEvent.Play(); break;
            case Verb.Loves: lovesEvent.Play(); break;
            case Verb.Serves: servesEvent.Play(); break;
        }
    }

    public void OnPanelIn()
    {
        Debug.Log("X");
    }

    internal void OnPanelOut()
    {
        Debug.Log("Y");
    }

    public void HandleGameOver(bool died, int jokesTold)
    {
        StartCoroutine(HandleGameOverDelayed(died, jokesTold));
    }

    IEnumerator HandleGameOverDelayed(bool died, int jokesTold)
    {
        panel.SetActive(false);

        yield return new WaitForSeconds(gameOverDelay);
        
        gameOverPanel.SetActive(true);
        gameOverText.text = died ? "YOU DIED" : "YOU WON";
        jokeCountText.text = $"YOU TOLD {jokesTold} JOKES";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("StartScene");
    }
}
