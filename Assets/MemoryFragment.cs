using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    // Köşeli parantezler [], bunun tek bir string değil, bir "string listesi" (Array) olduğunu söyler
    [TextArea(3, 5)] 
    public string[] memoryLines; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // Yöneticimize artık tek bir mesajı değil, tüm listeyi (memoryLines) gönderiyoruz
            DialogueManager.instance.ShowDialogue(memoryLines);
            
            Destroy(gameObject);
        }
    }
}