using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxLife;
    public float life;

    private int step = 1;

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
        if (life <= 0) //Morte
        {
            Destroy(gameObject);
        }

        Steps(); //Fases do inimigo

    }

    void Steps()
    {
        switch (step)
        {
            case 1:
                if (life <= 5)
                {
                    maxLife = 30;
                    life = 30;
                    animator.SetInteger("Step", 2);
                    step = 2;
                }
                break;

            case 2:
                if (life <= 1)
                {
                    maxLife = 75;
                    life = 75;
                    animator.SetInteger("Step", 3);
                    step = 3;
                }
                break;

            case 3:
                if (life <= 0)
                {
                    Destroy(this);
                }
                break;
        }
    }

}
