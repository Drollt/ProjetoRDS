using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float life;
    public float maxLife;

    // Update is called once per frame
    void Update()
    {
        life += Time.deltaTime;
        if (life >= maxLife)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
