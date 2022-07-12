using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System;
using UnityEngine.EventSystems;


public class DialogueManager : MonoBehaviour
{
    [Header("DialogueUI")]

    [SerializeField]
    private GameObject dialoguePanel;

    [SerializeField]
    private TextMeshProUGUI dialogueText;


    [Header("Choices UI")]
    [SerializeField]
    private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    public bool DialogueIsPlaying { get; private set; }



    private static DialogueManager instance;

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        DialogueIsPlaying = true;

        dialoguePanel.SetActive(true);

        ContinueStory();
    }
    

    private void ContinueStory()
    {
        
        if (currentStory.canContinue)
        {
            //write the story
            dialogueText.text = currentStory.Continue();

            //write choices

            DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        DialogueIsPlaying = false;

        dialoguePanel.SetActive(false);
        dialogueText.text = "";

    }

    private void DisplayChoices()
    {
        List<Ink.Runtime.Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("There are more choices than the UI supports");
        }


        int index = 0;


        foreach (Ink.Runtime.Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }


        StartCoroutine(SelectFirstChoice());

    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }


    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("There are more DialogManager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        
        DialogueIsPlaying = false;
        dialoguePanel.SetActive(false);


        choicesText = new TextMeshProUGUI[choices.Length];

        for (int index = 0; index < choices.Length; index++)
        {
            choicesText[index] = choices[index].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void FixedUpdate()
    {
        if (!DialogueIsPlaying)
        {
            return;
        }
        if (InputManager.GetInstance().GetSubmitPressed())
        {
           
            ContinueStory();
        }
    }
}
