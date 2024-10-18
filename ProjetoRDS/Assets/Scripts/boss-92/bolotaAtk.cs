using System.Collections;
using UnityEngine;

public class bolotaAtk : MonoBehaviour
{
    public GameObject projectilePrefab;  // projetil (bolota)
    public GameObject player;            // player
    public float orbitRadius = 1f;       // raio de Orbita
    public float orbitSpeed = 100f;      // velocidade de Orbita
    public float shootDelay = 3f;        // tempo orbitando
    public int numberOfProjectiles = 3;  // quantas bolotas (projetil) tem
    public float shootInterval = 1f;     // intervalo de tempo de cada disparo
    private GameObject[] projectiles;    // armazenamento de cada bolota (projetil)
    public BossRandomMovement bossMovementScript; //referencia de script de movimento do boss

    private void Start()
    {
        StartNewProjetileCycle(); //chama o primeiro ciclo das bolotas (projeteis)
    }

    private void StartNewProjetileCycle()  //metodo pra comecar a usar o novo ciclo das bolotas (projeteis)
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
        if (bossMovementScript != null) //desativa a movimentacao do boss assim que comeca a disparar
        {
            bossMovementScript.enabled = false; // desativa o movimento do boss
        }
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

                Destroy(projectiles[i], 2f); //destroi a bolota (projetil) lançado a cada 3 segundos

                yield return new WaitForSeconds(shootInterval); //espera o 'shootInterval' para disparar a proxima bolota (projetil)
            }
        }

        StartCoroutine(CheckAllProjectilesDestroyed()); //inicia uma corrotina que ve se todas as bolotas (projeteis) foram destruidas
    }

    private IEnumerator CheckAllProjectilesDestroyed() //corrotina responsavel por ver se todas as bolotas (projeteis) foram destruidas para iniciar o ciclo novamente
    {
        while (true)
        {
            bool allDestroyed = true; // esse 'for' e a variavel 'allDestroyed' basicamente fala que se existir bolotas (projeteis) a variavel sera 'false'
            for (int i = 0; i < projectiles.Length; i++)
            {
                if (projectiles[i] != null)
                {
                    allDestroyed = false;
                    break;
                }
            }
            if (allDestroyed) //se a variavel 'allDestroyed' for 'true' (nao existe mais bolotas(projeteis)) vai rodar esse if
            {
                if (bossMovementScript != null) //reativa o boss quando todos as bolotas (projeteis) forem disparados
                {
                    bossMovementScript.enabled = true; // reativa o movimento do boss
                }
                yield return new WaitForSeconds(6f); //pausa a corrotina (6 segundos)
                StartNewProjetileCycle(); //chama esse metodo para iniciar um novo ciclo de bolotas (projeteis)
                break;
            }
            yield return null;
        }
    }
}