/*using UnityEngine;

public class GunPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.PickUpGun();
                Destroy(gameObject); // remove crate
            }
        }
    }
}
*/