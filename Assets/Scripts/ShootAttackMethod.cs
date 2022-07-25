using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    internal class ShootAttackMethod : MonoBehaviour, IAttackMethod
    {
        //private float timer;

        private bool canAttack = true;

        [SerializeField]
        private Transform shootingPoint;

        [SerializeField]
        private float attackTimer = 10f;

        [SerializeField]
        private GameObject bullet;


        public void Attack()
        {
            if (canAttack)
            {
                PlaceProjectile();

                StartCoroutine(ReloadTime());
            }
        }


        private IEnumerator ReloadTime()
        {
            
            yield return new WaitForSeconds(attackTimer);
            canAttack = true;

        }

        private void PlaceProjectile()
        {
            GameObject go = Instantiate(bullet,shootingPoint.position, new Quaternion());

            
            

            go.GetComponent<Rigidbody2D>().AddForce(new Vector2(1f,0.5f) * 100);

            canAttack = false;
        }
    }
}
