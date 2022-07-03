using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class GroundDetector : MonoBehaviour, IGroundDetection
    {
        [SerializeField]
        private string groundTag;

        
        public bool OnGround { get; private set; }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals(groundTag))
            {
                OnGround = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            OnGround = false;
        }
    }
}
