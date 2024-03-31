using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player_Behavior : MonoBehaviour
{
    private Vector2 smooth_Movement;
    private Vector2 smooth_Update;
    [SerializeField] private float direction_change_rate = 0.1f;

    [SerializeField] private float move_speed;
    [SerializeField] private SwordAttack swordAttack;
    public ContactFilter2D movement_Filter; 
    [SerializeField] float collision_offset;
    [SerializeField] private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private int forward = 0;
    Vector2 movement_Input;
    private int count;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();   
        sprite= GetComponent<SpriteRenderer>();
        sprite.flipX = false;
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            if (movement_Input != Vector2.zero)
            {
                bool moved = TryMove(movement_Input);
                if (!moved)
                {
                    moved = TryMove(new Vector2(movement_Input.x, 0));
                }
                if (!moved)
                {
                    moved = TryMove(new Vector2(0f, movement_Input.y));
                }
                animator.SetBool("IsMoving", moved);
                animator.SetBool("WalkDown", Input.GetKeyDown(KeyCode.W));
                animator.SetBool("WalkDown", Input.GetKeyDown(KeyCode.S));
                SpriteFlip(movement_Input);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }
       
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            smooth_Movement = Vector2.SmoothDamp(smooth_Movement, direction, ref smooth_Update, direction_change_rate);
            this.count = rb.Cast(direction, movement_Filter, castCollisions, move_speed * Time.fixedDeltaTime + collision_offset);

            Vector2 actualPos = new Vector2(transform.position.x, transform.position.y);
            if (count == 0)
            {
                rb.MovePosition(actualPos + direction * move_speed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    private void OnMove(InputValue movementValue)
    {
        movement_Input = movementValue.Get<Vector2>();
    }
    private void SpriteFlip(Vector2 direction)
    {
        if (direction.x < 0)
        {
            sprite.flipX = true;
        }

        else if (direction.x > 0)
        {
            sprite.flipX = false; 
        }
    }
    private void OnFire()
    {
        animator.SetTrigger("SwordAttack");
    }
    private void SwordAttack()
    {
        LockMovement();
        if(sprite.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        } 
    }
    private void LockMovement()
    {
        canMove = false;
    }
    private void UnlockMovement()
    {
        canMove = true;
    }
    private void BringStop()
    {
        swordAttack.AttackStop();
    }

}
