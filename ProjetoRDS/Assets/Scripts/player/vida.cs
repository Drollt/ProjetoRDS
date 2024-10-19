using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Vida do jogador: " + health);

        if (health <= 0)
        {
            Debug.Log("Jogador morreu!");
            // Implementar lógica de morte, como reiniciar o jogo.
        }
    }
}
