using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class JumpAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D col;
    Animator animator;

    public Transform character;
    private Vector2 targetPosition;

    public float jumpForce = 5f;
    public Transform[] targetsShoot;

    public GameObject bulletPrefab;
    public float bulletSpeed = 10f; 

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character").transform;

        rb = this.GetComponent<Rigidbody2D>();

        col = this.GetComponent<BoxCollider2D>();

        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, targetPosition) <= 0.1) //calcular distancia entre player e boss
        {
            StopMovement();
        }
    }

    void Jump()
    {
        //Pulo pra cima do Player do Boss
        col.isTrigger = true;

        targetPosition = character.position;
        Vector2 direction = (targetPosition - rb.position).normalized;
        rb.AddForce(direction * jumpForce, ForceMode2D.Impulse);
    }

    void StopMovement()
    {
        // Para o pulo
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        col.isTrigger = false;  
    }

    void Shoot()
    {
        //Atira pra todas posições do targetsShoot
        col.isTrigger = false;
        foreach (Transform target in targetsShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = (target.position - transform.position).normalized;
            bulletRb.velocity = direction * bulletSpeed;

            Destroy(bullet, 2);
        }
    }

}
