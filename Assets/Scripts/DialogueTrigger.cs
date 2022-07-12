using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField]
    private GameObject visualCue;

    private IInRangeDetection inRangeDetection;

    private void FixedUpdate()
    {
        if (inRangeDetection.IsInRange)
        {
            visualCue.SetActive(true);
            
        }
        else
        {
            visualCue.SetActive(false);
        }
    }
}
