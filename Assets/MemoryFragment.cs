using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    [TextArea(3, 5)] 
    public string[] memoryLines; 

    [Header("Path Building Data")]
    // Bu anı okunduğunda hangi koordinatlara yol döşenecek?
    public Vector3Int[] tilesToBuild; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // Objenin görselini ve fiziğini kapatıyoruz (Yokmuş gibi davranacak ama kod çalışmaya devam edecek)
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // DialogueManager'a yazıları ve "Yazılar bitince OnDialogueFinished'ı çalıştır" emrini gönderiyoruz
            DialogueManager.instance.ShowDialogue(memoryLines, OnDialogueFinished);
        }
    }

    // Bu fonksiyonu biz doğrudan çağırmıyoruz, DialogueManager işi bitince çağırıyor
   private void OnDialogueFinished()
    {
        PathBuilder builder = FindFirstObjectByType<PathBuilder>();

        if (builder != null)
        {
            // Artık tek tek biz koymuyoruz, tüm listeyi PathBuilder'a verip "sırayla diz" diyoruz
            builder.BuildPathSequentially(tilesToBuild);
        }

        Destroy(gameObject);
    }
}