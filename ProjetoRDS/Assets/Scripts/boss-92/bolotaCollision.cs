using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolotaCollision : MonoBehaviour
{
    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            //Debug.Log("Bola de alma colidiu com o jogador!");
            Destroy(gameObject);

        }
    }

}
