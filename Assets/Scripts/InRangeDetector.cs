using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeDetector : MonoBehaviour, IInRangeDetection
{
    [SerializeField]
    private string playerTag = "Player";
    public bool IsInRange { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(playerTag))
        {
            IsInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(playerTag))
        {
            IsInRange = false;
        }
    }
}
