using UnityEngine;

public class BossRandomMovement : MonoBehaviour
{
    public float speed = 2f;              // Velocidade de movimento do boss
    public float changeDirectionTime = 2f; // Tempo entre mudanças de direção
    private Vector2 minBounds;            // Limites mínimos da área de movimento
    private Vector2 maxBounds;            // Limites máximos da área de movimento
    private Vector2 targetPosition;       // Posição alvo do boss
    private float timer;                  // Temporizador para mudança de direção

    private void Start()
    {
        // Definir os limites da área com base na câmera
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;  // Altura total da câmera
        float camWidth = camHeight * cam.aspect;      // Largura total da câmera

        // Metade superior da tela:
        float halfCamHeight = camHeight / 2;

        // Definir os limites mínimos e máximos da área de movimento
        minBounds = new Vector2(cam.transform.position.x - camWidth / 2, cam.transform.position.y); // Parte inferior da metade da tela
        maxBounds = new Vector2(cam.transform.position.x + camWidth / 2, cam.transform.position.y + halfCamHeight); // Parte superior da metade da tela

        // Definir o primeiro destino aleatório
        SetRandomTargetPosition();
        timer = changeDirectionTime;
    }

    private void Update()
    {
        // Movimentar o boss em direção à posição alvo
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Se o boss atingir a posição alvo, definir um novo destino
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }

        // Contar o tempo para mudar de direção aleatoriamente
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SetRandomTargetPosition();
            timer = changeDirectionTime;
        }
    }

    // Define uma posição alvo aleatória dentro dos limites
    private void SetRandomTargetPosition()
    {
        float randomX = Random.Range(minBounds.x, maxBounds.x); // Aleatório dentro da largura
        float randomY = Random.Range(minBounds.y, maxBounds.y); // Aleatório dentro da altura (metade superior)
        targetPosition = new Vector2(randomX, randomY);         // Define o destino aleatório
    }

    // Opcional: Mostrar os limites no editor
    private void OnDrawGizmos()
    {
        if (Camera.main == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(minBounds.x, minBounds.y), new Vector2(maxBounds.x, minBounds.y));
        Gizmos.DrawLine(new Vector2(minBounds.x, maxBounds.y), new Vector2(maxBounds.x, maxBounds.y));
        Gizmos.DrawLine(new Vector2(minBounds.x, minBounds.y), new Vector2(minBounds.x, maxBounds.y));
        Gizmos.DrawLine(new Vector2(maxBounds.x, minBounds.y), new Vector2(maxBounds.x, maxBounds.y));
    }
}
