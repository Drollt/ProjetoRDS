using UnityEngine;

public class BossRandomMovement : MonoBehaviour
{
    public float speed = 2f;              // Velocidade de movimento do boss
    public float changeDirectionTime = 2f; // Tempo entre mudan�as de dire��o
    private Vector2 minBounds;            // Limites m�nimos da �rea de movimento
    private Vector2 maxBounds;            // Limites m�ximos da �rea de movimento
    private Vector2 targetPosition;       // Posi��o alvo do boss
    private float timer;                  // Temporizador para mudan�a de dire��o

    private void Start()
    {
        // Definir os limites da �rea com base na c�mera
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;  // Altura total da c�mera
        float camWidth = camHeight * cam.aspect;      // Largura total da c�mera

        // Metade superior da tela:
        float halfCamHeight = camHeight / 2;

        // Definir os limites m�nimos e m�ximos da �rea de movimento
        minBounds = new Vector2(cam.transform.position.x - camWidth / 2, cam.transform.position.y); // Parte inferior da metade da tela
        maxBounds = new Vector2(cam.transform.position.x + camWidth / 2, cam.transform.position.y + halfCamHeight); // Parte superior da metade da tela

        // Definir o primeiro destino aleat�rio
        SetRandomTargetPosition();
        timer = changeDirectionTime;
    }

    private void Update()
    {
        // Movimentar o boss em dire��o � posi��o alvo
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Se o boss atingir a posi��o alvo, definir um novo destino
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }

        // Contar o tempo para mudar de dire��o aleatoriamente
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SetRandomTargetPosition();
            timer = changeDirectionTime;
        }
    }

    // Define uma posi��o alvo aleat�ria dentro dos limites
    private void SetRandomTargetPosition()
    {
        float randomX = Random.Range(minBounds.x, maxBounds.x); // Aleat�rio dentro da largura
        float randomY = Random.Range(minBounds.y, maxBounds.y); // Aleat�rio dentro da altura (metade superior)
        targetPosition = new Vector2(randomX, randomY);         // Define o destino aleat�rio
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
