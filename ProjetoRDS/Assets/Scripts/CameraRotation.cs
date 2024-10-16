using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Transform rotationCenter;
    public float distanceFromCenter = 1f;
    public float minMouseDistance = 210f; // Dist�ncia m�xima entre o mouse e o objeto para atualizar a rota��o

    private void Update()
    {
        // Obt�m a posi��o do mouse na tela
        Vector3 mousePos = Input.mousePosition;

        // Obt�m a posi��o do objeto no espa�o de tela
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        // Calcula o vetor de offset entre o objeto e a posi��o do mouse
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);

        // Calcula a dist�ncia entre o mouse e o objeto
        float mouseDistance = offset.magnitude;

        //Debug.Log(mouseDistance);

        // Verifica se o mouse est� a uma dist�ncia segura antes de atualizar a rota��o
        if (mouseDistance > minMouseDistance)
        {
            // Calcula o �ngulo em radianos
            float angle = Mathf.Atan2(offset.y, offset.x);

            // Converte o �ngulo de radianos para graus
            float angleDegrees = angle * Mathf.Rad2Deg;

            // Define a rota��o do objeto
            transform.rotation = Quaternion.Euler(0, 0, angleDegrees);

            // Movimenta o jogador para a borda maxima
            float currentAngle = Mathf.Deg2Rad * angleDegrees;
            Vector3 desiredPosition = rotationCenter.position + new Vector3(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle)) * distanceFromCenter;
            transform.position = desiredPosition;
        }
    }
}
