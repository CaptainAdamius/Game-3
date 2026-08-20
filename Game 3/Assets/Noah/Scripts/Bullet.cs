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


        //pass through damage to the hit object.
    }

    public void Damage(float bulletDamage)
    {
        bulletDamage = 1;
        Destroy(gameObject);
    }

    
}
