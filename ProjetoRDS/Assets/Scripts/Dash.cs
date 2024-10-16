using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private TrailRenderer trailRenderer;
    private Rigidbody2D rigidbody;

    [SerializeField] private float dashingVel = 14f;
    [SerializeField] private float dashingTime = 0.25f;
    private Vector2 dashinghDir;
    private bool isDashing;
    private float canDash = 1;

    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(canDash);
        var dashInput = Input.GetKey(KeyCode.LeftShift);

        if (dashInput && canDash >= 1)
        {
            isDashing = true;
            canDash = 0;
            //trailRenderer.emitting = true;
            dashinghDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if(dashinghDir == Vector2.zero)
            {
                dashinghDir = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(StopDashing());
        
        }else if (canDash < 1)
        {
            canDash += Time.deltaTime;
            GetComponent<TopDownController>().enabled = true;
        }

        //animator.SetBoll("IsDashing", isDashing);

        if (isDashing)
        {
            GetComponent<TopDownController>().enabled = false;
            rigidbody.velocity = dashinghDir.normalized * dashingVel;
            return;
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        //trailRenderer.emitting = false;
        isDashing = false;
    }

}
