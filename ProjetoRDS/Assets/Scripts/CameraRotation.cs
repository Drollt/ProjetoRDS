using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Transform rotationCenter;
    public float distanceFromCenter = 1f;
    public float minMouseDistance = 210f; // Distância máxima entre o mouse e o objeto para atualizar a rotação

    private void Update()
    {
        // Obtém a posição do mouse na tela
        Vector3 mousePos = Input.mousePosition;

        // Obtém a posição do objeto no espaço de tela
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        // Calcula o vetor de offset entre o objeto e a posição do mouse
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);

        // Calcula a distância entre o mouse e o objeto
        float mouseDistance = offset.magnitude;

        //Debug.Log(mouseDistance);

        // Verifica se o mouse está a uma distância segura antes de atualizar a rotação
        if (mouseDistance > minMouseDistance)
        {
            // Calcula o ângulo em radianos
            float angle = Mathf.Atan2(offset.y, offset.x);

            // Converte o ângulo de radianos para graus
            float angleDegrees = angle * Mathf.Rad2Deg;

            // Define a rotação do objeto
            transform.rotation = Quaternion.Euler(0, 0, angleDegrees);

            // Movimenta o jogador para a borda maxima
            float currentAngle = Mathf.Deg2Rad * angleDegrees;
            Vector3 desiredPosition = rotationCenter.position + new Vector3(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle)) * distanceFromCenter;
            transform.position = desiredPosition;
        }
    }
}
