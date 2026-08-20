using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;



public enum EnemyState { Idle, Attack, Dead }

public class Enemy : MonoBehaviour
{

    public EnemyState enemyState;



    [SerializeField] float enemyHealth;


    [SerializeField] float rayDistance;
    [SerializeField] LayerMask hitLayer;
    [SerializeField] Transform rayStartPos;
    Transform playerlocation;


    bool shooting;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate;
    [SerializeField] Transform spawnPoint;

     bool facingRight;

    Rigidbody2D rb;
    [SerializeField] float moveSpeed;

    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    private Transform currentPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleState();
    }


    void HandleState()
    {
        detectPlayer();

        if(enemyHealth <=0) enemyState = EnemyState.Dead;


        switch (enemyState)
        {
            case EnemyState.Idle:
                Idle();

                break;
      
            case EnemyState.Attack:
                Attack();

                break;
            case EnemyState.Dead:
                dead();

                break;
        }
    }

    void Idle()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(moveSpeed, 0f);
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            facingRight = true;
        }
        else
        {
            rb.linearVelocity = new Vector2(-moveSpeed, 0f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingRight = false;
        }


        if(Vector2.Distance(transform.position, currentPoint.position)<0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;

        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }

    }


    void Attack()
    {
        Vector2 direction = (transform.position - playerlocation.position).normalized;
        if(direction.x<0)
        {
            facingRight = true;
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else
        {
            facingRight = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
            

        if (!shooting) StartCoroutine(Shoot());
    }
 

    void dead()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float bulletDamage)
    {
        enemyHealth -= bulletDamage;
    }


    void detectPlayer()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(rayStartPos.position, -transform.right, rayDistance, hitLayer);
        playerlocation = hit.transform;

        if (hit)
        {
            enemyState = EnemyState.Attack;
        }
        else
        {
            enemyState = EnemyState.Idle;
        }
    }




    IEnumerator Shoot()
    {
        shooting = true;

        Debug.Log("Shoot");

        float angle = facingRight ? 0f : 180f;
        Instantiate(bulletPrefab, spawnPoint.position, Quaternion.Euler(0, 0, angle));
        //Instantiate(bulletPrefab, spawnPoint.position, Quaternion.Euler(0, 0, 0));

        yield return new WaitForSeconds(fireRate);

        shooting = false;
    }
   

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayStartPos.position,-transform.right * rayDistance);  
    }


  
}
