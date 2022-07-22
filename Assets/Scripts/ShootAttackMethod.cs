using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    internal class ShootAttackMethod : MonoBehaviour, IAttackMethod
    {
        //private float timer;

        private bool canAttack = true;

        [SerializeField]
        private float attackTimer = 1f;

        [SerializeField]
        private GameObject bullet;


        public void Attack()
        {
            if (canAttack)
            {
                PlaceProjectile();

                ReloadTime();
            }
        }


        private IEnumerator ReloadTime()
        {
            canAttack = false;
            yield return new WaitForSeconds(attackTimer);
            canAttack = true;

        }

        private void PlaceProjectile()
        {
            Instantiate(bullet);
        }
    }
}
