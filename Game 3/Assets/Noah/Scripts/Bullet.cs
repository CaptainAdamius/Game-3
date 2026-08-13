using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    Rigidbody2D rb;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletDaamge;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //rb.linearVelocity = transform.forward * bulletSpeed;
    }  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        //pass through damage to the hit object
    }
}
