using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidefire : MonoBehaviour
{
     
     public int damage = 10;     // Dano que o projétil causa

    

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o projétil colidiu com o jogador
        if (other.CompareTag("Player"))
        {
            // Destrói o projétil
            Destroy(gameObject);

            other.GetComponent<vida>()?.TakeDamage(damage);
        }
    }      
}
