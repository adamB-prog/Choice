using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueWriter : MonoBehaviour, IDialogueWriter
{
    [SerializeField]
    private float secondsBetweenCharacters = 0.1f;

    public bool IsFinished { get; private set; } = true;

    public void WriteDialogueText(TextMeshProUGUI tmpro, string text)
    {
        IsFinished = false;
        DialogueManager.GetInstance().ClearDialogueText();
        StartCoroutine(WriteLetterByLetter(tmpro, text));
    }


    private IEnumerator WriteLetterByLetter(TextMeshProUGUI tmpro, string text)
    {
        foreach (var character in text)
        {
            tmpro.text += character;
            yield return new WaitForSeconds(secondsBetweenCharacters);
        }
        IsFinished = true;
        DialogueManager.GetInstance().DisplayChoices();
    }
}
