using UnityEngine;

public class MemoryItem : MonoBehaviour
{
    [Header("Item Settings")]
    [Tooltip("Bu parçanın adı ne? (Örn: Bilet Parçası 1)")]
    public string itemName = "Memory Piece";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Eğer çarpan kişi Player ise...
        if (collision.gameObject.name == "Player")
        {
            Debug.Log(itemName + " toplandı!");

            // Oyun Yöneticimize (GameManager) bir parça bulduğumuzu haber veriyoruz
            GameManager.instance.AddMemoryPiece();

            // Parçayı sahneden yok et (Çünkü topladık)
            Destroy(gameObject);
        }
    }
}