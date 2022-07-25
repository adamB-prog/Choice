using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System;
using UnityEngine.EventSystems;
using Assets.Scripts.Interfaces;
using System.Threading;

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


    private IDialogueWriter dialogueWriter;
    

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

            HideChoiceUI();

            //if there is a custom writer then...
            if (dialogueWriter is not null)
            {
                dialogueWriter.WriteDialogueText(dialogueText, currentStory.Continue());
            }
            else
            {   
                //else use the default
                //write story
                WriteDialogueText(currentStory.Continue());

                //write choices
                DisplayChoices();
            }
            
            


            
            

            
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    //Default Writer
    private void WriteDialogueText(string text)
    {
        dialogueText.text = text;
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        DialogueIsPlaying = false;

        dialoguePanel.SetActive(false);
        dialogueText.text = "";

    }

    public void DisplayChoices()
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
    public void SelectNoneChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void HideChoiceUI()
    {
        foreach (var choiceUI in choices)
        {
            choiceUI.SetActive(false);
        }
    }

    public void ClearDialogueText()
    {
        dialogueText.text = String.Empty;
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }


    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("There are more DialogueManager in the scene");
        }
        instance = this;


        dialogueWriter = GetComponent<IDialogueWriter>();
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
        if (!dialogueWriter.IsFinished)
        {
            return;
        }
        if (InputManager.GetInstance().GetSubmitPressed())
        {
           
            ContinueStory();
        }
    }
}
