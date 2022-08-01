using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    internal class ShootAttackMethod : MonoBehaviour, IAttackMethod
    {
        

        private bool canAttack = true;

        [SerializeField]
        private Transform shootingPoint;


        [Header("Weapon params")]
        [SerializeField]
        private float reloadTime = 10f;

        [SerializeField]
        private float shootForce;

        [SerializeField]
        private GameObject bullet;


        public void Attack(Vector2 rotation)
        {
            if (canAttack)
            {
                PlaceProjectile(rotation);

                StartCoroutine(ReloadTime());
            }
        }


        private IEnumerator ReloadTime()
        {
            
            yield return new WaitForSeconds(reloadTime);
            canAttack = true;

        }

        private void PlaceProjectile(Vector2 rotation)
        {

            Vector2 vec = rotation - new Vector2(shootingPoint.position.x, shootingPoint.position.y);
            
            GameObject go = Instantiate(bullet,shootingPoint.position, new Quaternion(vec.x,vec.y, 0, 0));


            

            go.GetComponent<Rigidbody2D>().AddForce(vec * shootForce);

            canAttack = false;
        }
    }
}
