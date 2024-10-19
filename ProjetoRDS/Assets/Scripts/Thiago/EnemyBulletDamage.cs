using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletDamage : MonoBehaviour
{
    public float bulletDamage;

    private int collisions = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.transform.tag == "Character")
        {
            collision.gameObject.GetComponent<EntityStats>().life -= bulletDamage;
            Destroy(this.gameObject);
        }
        else if(collision.transform.tag != "Bullet" && collision.transform.tag != "EnemyBullet")
        {
            collisions++;
            if(collisions >= 2)
            {
                Destroy(this.gameObject);
            }
        }


        Destroy(this.gameObject, 3);
    }
}
