using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RD : MonoBehaviour
{
    public float detectionRadius = 5f;   // Raio de detecção
    public float stopDistance = 1f;      // Distância mínima para parar de se mover
    public float minDistance = 1f;       // Distância mínima entre inimigo e jogador
    public GameObject projectilePrefab;  // Prefab do projétil
    public Transform firePoint;          // Ponto de disparo do projétil
    public float projectileSpeed = 10f;  // Velocidade do projétil
    public float attackInterval = 2f;    // Intervalo entre ataques
    public float moveSpeed = 3f;         // Velocidade de movimento do inimigo

    public int maxHealth = 10;            // Vida máxima do inimigo
    private int currentHealth;             // Vida atual do inimigo
    public Text healthText;                // Referência para o texto de vida na UI

    private Transform player;            // Referência ao Transform do jogador
    private bool playerInRange = false;  // Se o jogador está no raio de detecção
    private int difficultyLevel = 1;       // Nível de dificuldade atual

    void Start()
    {
        currentHealth = maxHealth;          // Inicializa a vida do inimigo
        UpdateHealthUI();                   // Atualiza a UI no início
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        DetectPlayer();
        if (playerInRange)
        {
            MoveTowardsPlayer();  // Perseguir o jogador se ele estiver no raio
        }
    }

    void DetectPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRadius && !playerInRange)
        {
            playerInRange = true;
            StartCoroutine(AttackPlayer());  // Começar a atacar
        }
        else if (distance > detectionRadius)
        {
            playerInRange = false;
            StopAllCoroutines();  // Parar ataques se o jogador sair do raio
        }
    }

    void MoveTowardsPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        // Se o inimigo estiver longe o suficiente, ele se move em direção ao jogador
        if (distance > minDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position,player.position,moveSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduz a vida do inimigo
        UpdateHealthUI();         // Atualiza a UI após dano

        if (currentHealth <= 0)
        {
            StartCoroutine(HandleDeath()); // Inicia o processo de morte
        }
    }
    

    IEnumerator AttackPlayer()
    {
        

        while (playerInRange)
        {
            LaunchProjectile();
            yield return new WaitForSeconds(attackInterval);  // Espera antes do próximo ataque
        }

        
    }

    IEnumerator HandleDeath()
    {
        // Lógica para retornar o inimigo mais difícil
        yield return new WaitForSeconds(1f); // Espera um segundo antes de renascer

        difficultyLevel++; // Aumenta o nível de dificuldade
        currentHealth = maxHealth + difficultyLevel * 5; // Aumenta a vida do inimigo com base na dificuldade
        UpdateHealthUI(); // Atualiza a UI com a nova vida

        
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Vida: " + currentHealth; // Atualiza o texto da UI
        }
    }

    void LaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Define a velocidade do projétil
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }


}
