using System.Collections; // Coroutine kullanmak için şart
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Siyah perdeyi (UI) kodda tanımak için şart!

public class MemoryGateway : MonoBehaviour
{
    [Header("Teleport Settings")]
    public string nextMemorySceneName;

    [Header("Cinematic Transition")]
    [Tooltip("Canvas içindeki FadeScreen objesini buraya sürükleyin")]
    public Image fadeScreen; 
    public float fadeDuration = 1.0f; // Ekranın kararma süresi (saniye)

    private bool isFading = false; // Kararma başladıktan sonra oyuncu tekrar çarparsa kodu bozmasın diye bir güvenlik kilidi

private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collided and the fade animation hasn't started yet
        if (collision.gameObject.name == "Player" && !isFading)
        {
            // Check if the gateway is unlocked (all pieces are collected)
            if (GameManager.instance.isGatewayOpen)
            {
                Debug.Log("Gateway is open! Transitioning to the next scene...");
                StartCoroutine(FadeAndLoad());
            }
            else
            {
                // Inform the player if they haven't collected enough pieces
                Debug.Log("The gateway is locked! You need to find all missing memory pieces first.");
            }
        }
    }

    private IEnumerator FadeAndLoad()
    {
        isFading = true; // Kapıyı kilitledik, animasyon başladı

        // Perdenin mevcut rengini hafızaya alıyoruz
        Color fadeColor = fadeScreen.color;
        float elapsedTime = 0f;

        // Belirlediğimiz süre (1 saniye) boyunca adım adım Alpha (Saydamlık) değerini 0'dan 1'e çıkartıyoruz
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(elapsedTime / fadeDuration); // Saydamlığı zamanla orantılı artır
            fadeScreen.color = fadeColor;
            
            yield return null; // Bir sonraki Frame'e (kareye) kadar bekle
        }

        // Ekran tamamen simsiyah oldu! Artık oyuncuyu diğer sahneye güvenle atabiliriz
        SceneManager.LoadScene(nextMemorySceneName);
    }
}