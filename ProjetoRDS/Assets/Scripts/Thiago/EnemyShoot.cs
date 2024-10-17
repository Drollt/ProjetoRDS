using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : StateMachineBehaviour
{
    public GameObject bulletPrefab;  

    private Transform player;

    public float bulletSpeed = 5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Character").transform;

        Shoot(animator.transform.position);
    }

    void Shoot(Vector3 position)
    {

        if (player != null)
        {
            //Tiro do inimigo no player

            GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);

            Vector2 direction = (player.position - position).normalized;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bulletSpeed;

        }
        
    }
}
