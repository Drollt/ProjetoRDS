using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class JumpAttack : StateMachineBehaviour
{
    private Rigidbody2D rb;

    public Transform player;

    public float speedJump;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Character").transform;

        rb = animator.GetComponent<Rigidbody2D>();

        Jump();
    }

    void Jump()
    {
        if(player != null)
        {
            Vector2 target = new Vector2(player.position.x, player.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speedJump * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
    }
}
