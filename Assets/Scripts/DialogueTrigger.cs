using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField]
    private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField]
    private TextAsset inkJSON;

    [Header("TriggerSettings")]
    [SerializeField]
    private bool disableAfterStart = true;

    


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
                if (disableAfterStart)
                {
                    this.enabled = false;
                    visualCue.SetActive(false);
                }

                
            }
            
        }
        else
        {
            visualCue.SetActive(false);
        }
    }


}
