using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteoro : MonoBehaviour
{
   public GameObject meteorPrefab;  // Prefab do meteoro
    public float spawnInterval = 10f; // Tempo entre cada spawn
    public float spawnRadius = 20f;  // Distância máxima para spawnar ao redor do mapa

    void Start()
    {
        // Começa o spawn de meteoros repetidamente
        StartCoroutine(SpawnMeteors());
    }

    IEnumerator SpawnMeteors()
    {
        while (true)
        {
            SpawnMeteor();
            yield return new WaitForSeconds(spawnInterval); // Intervalo entre cada spawn
        }
    }

    void SpawnMeteor()
    {
        // Gera uma posição aleatória ao redor do mapa dentro do raio definido
        Vector2 randomPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

        // Cria o meteoro nessa posição aleatória
        Instantiate(meteorPrefab, randomPosition, Quaternion.identity);
    }

}
