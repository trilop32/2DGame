using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Персонаж упал в бездну!");

            // Найдём PlayerController и вызовем Die()
            PlayerController player = FindAnyObjectByType<PlayerController>();
            if (player != null)
            {
                player.Die();
            }
            else
            {
                Debug.LogError("❌ PlayerController не найден!");
            }
        }
    }
}