using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public enum GunType { pistol, rpg }
    public GunType gunType;


    [SerializeField] float fireRate;
    private bool shooting;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnPoint;

    private Rigidbody2D bulletRb;
    [SerializeField] float bulletSpeed;
    
    private PlayerController playercontroller;

    private Vector2 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shooting = false;
        playercontroller = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (gunType)
        {
            case GunType.pistol:

                fireRate = 0.5f;
                break;
            case GunType.rpg:

                fireRate = 2f;
                break;
        }




        if (Input.GetKey(KeyCode.Z) && !shooting)
        {
            StartCoroutine(Shoot());
        }

    }

    IEnumerator Shoot()
    {
        shooting = true;

        Debug.Log("Shoot");
        
        Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        bulletRb = bulletPrefab.GetComponent<Rigidbody2D>();
        
        ShootDirection();

        bulletRb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
  
        yield return new WaitForSeconds(fireRate);

        shooting = false;
    }

    private void ShootDirection()
    {
        if(playercontroller.facingRight)
        {
            direction = transform.right;
        }
        else
        {
            direction = -transform.right;
        }
    }


    // bullet prefab

    //When player presses "z"
    //instantiate bullet prefab
    //shooting = true
    //float firerate

    //Timer
    //bool shooting
    //coroutine 
    // while is shooting is false
    //return

    //wait seconds (firerate)

    //shooting = false
    //return
   



}
