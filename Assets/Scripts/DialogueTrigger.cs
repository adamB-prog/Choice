using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField]
    private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField]
    private TextAsset inkJSON;




    private IInRangeDetection inRangeDetection;

    private void Awake()
    {
        inRangeDetection = GetComponent<IInRangeDetection>();
    }

    private void FixedUpdate()
    {
        if (inRangeDetection.IsInRange && !DialogueManager.GetInstance().DialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }


}
