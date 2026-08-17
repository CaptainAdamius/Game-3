using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public enum GunType { Pistol, Shotgun }
    public GunType gunType;


    [SerializeField] float fireRate;
    private bool shooting;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnPoint;

    private Rigidbody2D bulletRb;
    [SerializeField] float bulletSpeed;

    private PlayerController playerController;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shooting = false;
        playerController = GetComponentInParent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (gunType)
        {
            case GunType.Pistol:

                fireRate = 0.5f;
                if (Input.GetKey(KeyCode.Z) && !shooting)
                {
                    StartCoroutine(Shoot());
                }

                break;
            case GunType.Shotgun:

                fireRate = 2f;
                if (Input.GetKey(KeyCode.Z) && !shooting)
                {
                    StartCoroutine(Shotgun());
                }
                break;
        }

    }

    IEnumerator Shoot()
    {
        shooting = true;

        Debug.Log("Shoot");
        float angle = !playerController.facingRight ? 0f : 180f;
        Instantiate(bulletPrefab, spawnPoint.position, Quaternion.Euler(0,0, angle));
        
        yield return new WaitForSeconds(fireRate);

        shooting = false;
    }



    IEnumerator Shotgun()
    {
        shooting = true;


        float angle = !playerController.facingRight ? 0f : 180f;


        Quaternion rot1 = Quaternion.Euler(spawnPoint.transform.localEulerAngles.x,
        spawnPoint.transform.localEulerAngles.y,
        spawnPoint.transform.localEulerAngles.z + 10);

        Quaternion rot2 = Quaternion.Euler(spawnPoint.transform.localEulerAngles.x,
        spawnPoint.transform.localEulerAngles.y,
        spawnPoint.transform.localEulerAngles.z - 10);

        Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        Instantiate(bulletPrefab, spawnPoint.position, rot1);
        Instantiate(bulletPrefab, spawnPoint.position, rot2);


        yield return new WaitForSeconds(fireRate);

        shooting = false;
    }


    public void EnemyShoot()
    {
        StartCoroutine(Shoot());
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
