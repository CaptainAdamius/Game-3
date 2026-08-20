using Unity.VisualScripting;
using UnityEngine;

public class PlayerDestructibleTest : MonoBehaviour
{
// Literally just a trigger enter check.
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Destructible has collided with player.");
    }
}
