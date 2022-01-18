using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    
    public Transform attackPoint;
    [SerializeField] private float attackRange = 5.0f;

    private float horizontalMove = 0f;

    private Vector3 moveDirection = Vector3.zero;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    //#Movement

    private void Update()
    {
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 temp = transform.rotation.eulerAngles;
            temp.y = 0f;
            transform.rotation = Quaternion.Euler(temp);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 temp = transform.rotation.eulerAngles;
            temp.y = 180f;
            transform.rotation = Quaternion.Euler(temp);
        }
    }

    private void FixedUpdate()
    {
        characterController.Move(moveDirection * Time.deltaTime);
    }

    //#Combat System

    /*void Attack()
    {      
        StartCoroutine(AttackAnimation());      
    }*/

    /*IEnumerator AttackAnimation()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        rb.constraints = RigidbodyConstraints2D.FreezePosition;

        yield return new WaitForSeconds(0.6f);
        
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in Enemies)
        {
                enemy.GetComponent<Animator>().SetTrigger("Hurt");
                enemy.GetComponent<Enemy>().health -= 1;             
        }

        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        yield return new WaitForSeconds(0.2f);

        isAttacking = false;
    }*/

    //#Gizmos Hitbox

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
