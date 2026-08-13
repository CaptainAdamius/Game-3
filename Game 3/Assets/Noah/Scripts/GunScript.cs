using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    [SerializeField] float fireRate;
    private bool shooting;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnPoint;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shooting = false;
        
    }

    // Update is called once per frame
    void Update()
    {
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
        


        yield return new WaitForSeconds(fireRate);

        shooting = false;
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
