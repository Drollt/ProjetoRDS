using System.Collections;
using UnityEngine;

public class bolotaAtk : MonoBehaviour
{
    public GameObject projectilePrefab;  // projetil (bolota)
    public GameObject player;            // player
    public float orbitRadius = 1f;       // raio de Orbita
    public float orbitSpeed = 100f;      // velocidade de Orbita
    public float shootDelay = 3f;        // tempo em tela
    public int numberOfProjectiles = 3;  // quantas bolotas (projetil) tem
    public float shootInterval = 1f;     // intervalo de tempo de cada disparo
    private GameObject[] projectiles;    // armazenamento de cada bolota (projetil)

    private void Start()
    {
        projectiles = new GameObject[numberOfProjectiles]; // inicia o array com o numero de bolotas (projeteis) que tem no 'numberOfProjectiles'

        // esse for instancia as bolotas (projeteis) na posicao do boss. cada bolota (projetil) e uma instancia do 'projectilePrefab'
        // e cada um instanciado na array 'projectiles'
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            projectiles[i] = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }

        StartCoroutine(OrbitProjectiles()); // inicia uma corrotina e faz os projeteis orbitarem o boss

        Invoke("StartShootingProjectiles", shootDelay); //o invoke chama o 'StartShootingProjectiles' no tempo determinado do 'shootDelay'
    }

    private IEnumerator OrbitProjectiles() //define a corrotina 
    {
        float angleStep = 360f / numberOfProjectiles; // divide 360 graus pelo numero de bolotas (projeteis) que tem
        float currentAngle = 0f; // controla o angulo de cada bolota (projetil) em sua orbita

        while (true) // loop infinito garantindo que as bolotas (projeteis) continuem orbitando 
        {
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                if (projectiles[i] != null) // verifica se a bolota (projétil) ainda existe
                {
                    // calcula a posição da bolota (projétil) usando 'Mathf.Cos' e 'Mathf.Sin' para posicionar na orbita com o raio 'orbitRadius'
                    float posX = transform.position.x + Mathf.Cos(Mathf.Deg2Rad * currentAngle) * orbitRadius;
                    float posY = transform.position.y + Mathf.Sin(Mathf.Deg2Rad * currentAngle) * orbitRadius;

                    projectiles[i].transform.position = new Vector3(posX, posY, transform.position.z); // atualiza a posição do projétil
                    currentAngle += angleStep; //atualiza o angulo pra proxima posicao na orbita
                }
            }

            currentAngle += orbitSpeed * Time.deltaTime; //atualiza o angulo conforme a velocidade de orbita multiplicando em relacao ao ultimo frame

            yield return null; // faz a corrotina espera até o próximo frame
        }
    }

    private void StartShootingProjectiles() //metodo que chamado apos o 'shootDelay'
    {
        StopAllCoroutines(); // para todas as corrotinas ativas
        StartCoroutine(ShootProjectiles()); // inicia uma nova corrotina para disparar as bolotas (projeteis)
    }

    private IEnumerator ShootProjectiles() //corrotina responsavel por disparar bolota por bolota (projetil por projetil)
    {
        for (int i = 0; i < numberOfProjectiles; i++) //loop que repete para todos as bolotas (projeteis)
        {
            if (projectiles[i] != null) 
            {
                // solta a bolota (projétil) da órbita e faz ele ir em direção ao player
                Vector3 directionToPlayer = (player.transform.position - projectiles[i].transform.position).normalized;
                Rigidbody2D rb = projectiles[i].GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    rb.velocity = directionToPlayer * 10f; // define a velocidade do bolota (projétil) em direção ao player
                }

                Destroy(projectiles[i], 5f); //destroi a bolota (projetil) lançado a cada 5 segundos

                yield return new WaitForSeconds(shootInterval); //espera o 'shootInterval' para disparar a proxima bolota (projetil)
            }
        }
    }
}
