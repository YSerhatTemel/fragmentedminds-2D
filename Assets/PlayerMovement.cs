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
        // 1. DİYALOG KONTROLÜ (Girdileri Kes ve Zihni Temizle)
        if (DialogueManager.instance.isDialogueActive)
        {
            // ÇOK ÖNEMLİ: Karakterin en son hatırladığı hareket yönünü tamamen sıfırlıyoruz.
            // Böylece FixedUpdate'teki hesaplama sıfırla çarpılıp karakteri durduracak.
            movement = Vector2.zero; 
            
            return; // Aşağıdaki tuş okuma kodlarını iptal et
        }

        // GİRDİ ALMA: Klavyeden yön tuşlarını (WASD veya Ok tuşları) dinliyoruz
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Çapraz giderken vektör uzunluğunun artmasını önlemek için normalize ediyoruz
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // 2. FİZİK KONTROLÜ (Fizik motorunu da dondur)
        if (DialogueManager.instance.isDialogueActive)
        {
            rb.linearVelocity = Vector2.zero; // Varsa ekstra savrulmaları/kaymaları durdur
            return; // Fizik motorunun karakteri hareket ettirmesini tamamen iptal et
        }

        // HAREKET ETME: Fizik motoruyla ilgili hesaplamalar
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}