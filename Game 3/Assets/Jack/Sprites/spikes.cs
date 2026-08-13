using UnityEngine;

public class Spikes : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("Tag used to identify the player object.")]
    [SerializeField] private string playerTag = "Player";

    [SerializeField] Transform spawnPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Triggers if the spike collider is SOLID
        if (collision.gameObject.CompareTag(playerTag))
        {
            Debug.Log("collision detected with: " + collision.gameObject.name);

            collision.transform.position = spawnPoint.transform.position;
        }
    }

}

