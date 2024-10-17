using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxLife;
    public float life;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0) //Morte
        {
            Destroy(gameObject);
        }

        Steps(); //Fases do inimigo

    }

    void Steps()
    {
        if(life <= 5)
        {
            maxLife = 50;
            life = 50;
            animator.SetInteger("Step", 2);
        }
    }

}
