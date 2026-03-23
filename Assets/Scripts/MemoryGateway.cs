using UnityEngine;
using UnityEngine.SceneManagement; // Sahneler arası geçiş için gerekli kütüphane

public class MemoryGateway : MonoBehaviour
{
    [Header("Teleport Settings")]
    [Tooltip("Gidilecek yeni sahnenin tam adını buraya yazın")]
    public string nextMemorySceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarpan kişi bizim karakterimiz mi?
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Anıya geçiş yapılıyor: " + nextMemorySceneName);
            
            // Eğer aynı sahnede başka bir koordinata ışınlayacaksak kod farklı olur, 
            // ama yeni bir sahneye (yepyeni bir anı odasına) geçeceksek bunu kullanırız:
            SceneManager.LoadScene(nextMemorySceneName);
        }
    }
}