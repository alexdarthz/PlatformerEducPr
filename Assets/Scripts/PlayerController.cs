using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject TargetGround;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded = false;

    private void Awake()
    {
        TargetGround = GameObject.Find("TargetGround");
        sprite = this.GetComponentInChildren<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }

    private States State
    {
        get { return (States)anim.GetInteger("status"); }
        set { anim.SetInteger("status", (int)value); }
    }

    public enum States
    {
        idle,
        run,
        jump
    }

    private void FixedUpdate()
    {
        isGround();
    }

    private void Update()
    {
        if (isGrounded) State = States.idle;
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
    }

    private void Run()
    {
        if (isGrounded) State = States.run;

        Vector3 dir = this.transform.right * Input.GetAxis("Horizontal");
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + dir, Config.speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    private void isGround()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(TargetGround.transform.position, 0.4f);
        isGrounded = coll.Length > 1;
    }

    private void Jump()
    {
        isGrounded = false;
        rb.AddForce(this.transform.up * Config.jumpForce, ForceMode2D.Impulse);
        State = States.jump;
    }
}
