using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    Rigidbody2D rb;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletDamage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = transform.right * bulletSpeed;
        
    }  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Get the EnemyHealth component and deal damage
            if (collision.TryGetComponent<Enemy>(out var Enemy))
            {
                Enemy.TakeDamage(bulletDamage);
            }

            // Destroy the bullet on impact
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            // Get the EnemyHealth component and deal damage
            if (collision.TryGetComponent<PlayerController>(out var Player))
            {
                Player.TakeDamage(bulletDamage);
            }

            // Destroy the bullet on impact
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

 


   
    

    
}
