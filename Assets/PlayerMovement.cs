using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Karakterin hızı (Unity arayüzünden de değiştirilebilir)
    private Rigidbody2D rb;
    private Vector2 movement; // X ve Y eksenindeki hareket yönümüz

    void Start()
    {
        // Oyun başladığında objenin üzerindeki Rigidbody2D bileşenini bul ve değişkene ata
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // GİRDİ ALMA: Klavyeden yön tuşlarını (WASD veya Ok tuşları) dinliyoruz
        // Basılmazsa 0, basılırsa yönüne göre 1 veya -1 döndürür.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Çapraz giderken vektör uzunluğunun artmasını (karakterin hızlanmasını) önlemek için normalize ediyoruz
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // HAREKET ETME: Fizik motoruyla ilgili hesaplamalar her zaman FixedUpdate içinde yapılır
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}