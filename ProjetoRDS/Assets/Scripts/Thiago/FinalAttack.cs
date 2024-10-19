using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FinalAttack : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform character;

    public Transform[] SideTargets;
    public Transform[] FrontTargets;

    public float bulletSpeed;
    private bool Attack = false;
    private float timeAttack = 0;
    private int quantAttack = 0;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(quantAttack);
        if (Attack)
        {
            //Ativa o RandomAttack
            RandomAttack();
        }
    }

    void CanAttack()
    {
        //Permite o Ataque
        Attack = true;
    }

    void RandomAttack()
    {
        //Selecione aleatoriamente o ataque
        timeAttack += Time.deltaTime;
        if (timeAttack >= 2)
        {
            quantAttack = Random.Range(1, 4);
            if (quantAttack == 1)
            {
                PlayerAttack();
            }
            else if (quantAttack == 2)
            {
                SideAttack();
            }
            else
            {
                FrontAttack();
            }
            timeAttack = 0;
        }
        
    }

    void PlayerAttack()
    {      
        //Atira no player de cima e de lado

        Vector3 yUpPosition = new Vector3(character.transform.position.x, character.transform.position.y + 5, character.transform.position.z);
        Vector3 yDownPosition = new Vector3(character.transform.position.x, character.transform.position.y - 5, character.transform.position.z);
        Vector3 xRightPosition = new Vector3(character.transform.position.x + 5, character.transform.position.y, character.transform.position.z);
        Vector3 xLeftPosition = new Vector3(character.transform.position.x - 5, character.transform.position.y, character.transform.position.z);

        Vector3[] AllPositions = new Vector3[] { yUpPosition, yDownPosition, xRightPosition, xLeftPosition };

        foreach (Vector3 position in AllPositions)
        {
            GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = (character.position - position).normalized;
            bulletRb.velocity = direction * bulletSpeed;

            Destroy(bullet, 2);
        }
    }

    void SideAttack()
    {
        //Atira de lado em 4 posições
        foreach (Transform position in SideTargets)
        {
            GameObject bullet = Instantiate(bulletPrefab, position.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction;
            if (position.position.x > 0)
            {
                direction = (new Vector3(-1, 0, 0)).normalized;
            }
            else
            {
                direction = (new Vector3(1, 0, 0)).normalized;
            }
            bulletRb.velocity = direction * bulletSpeed;

            Destroy(bullet, 2);
        }
    }

    void FrontAttack()
    {
        //Atira de cima de 4 posições 
        foreach (Transform position in FrontTargets)
        {
            GameObject bullet = Instantiate(bulletPrefab, position.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction;
            if (position.position.y > 0)
            {
                direction = (new Vector3(0, -1, 0)).normalized;
            }
            else
            {
                direction = (new Vector3(0, 1, 0)).normalized;
            }
            bulletRb.velocity = direction * bulletSpeed;

            Destroy(bullet, 2);
        }
    }
}
