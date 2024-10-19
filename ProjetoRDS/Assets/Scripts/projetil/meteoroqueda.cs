using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteoroqueda : MonoBehaviour
{
    public float fallSpeed = 5f;  // Velocidade de queda
    public int damage = 20;       // Dano causado ao jogador
    public float lifetime = 7f;   // Tempo até o meteoro se destruir automaticamente

    private Transform player;     // Referência ao jogador

    void Start()
    {
        // Acha o jogador pela tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Destrói o meteoro automaticamente após 'lifetime' segundos
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move o meteoro em direção ao jogador
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * fallSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Se o meteoro colidir com o jogador
        if (other.CompareTag("Player"))
        {
            // Acessa o componente de vida do jogador para causar dano
            other.GetComponent<vida>().TakeDamage(damage);

            // Destroi o meteoro após causar dano
            Destroy(gameObject);
        }
    }
}
