using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    [TextArea(3, 5)] 
    public string[] memoryLines; 

    [Header("Path Building Data")]
    public Vector3Int[] tilesToBuild; 

    [Header("Choice Mechanics")]
    [Tooltip("Eğer bu anı seçilirse, YIKILACAK olan 'diğer' anıyı buraya sürükleyin")]
    public GameObject oppositeFragment; // Rakip anı

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // SEÇİM MEKANİĞİ: Eğer bu anının bir "rakibi" varsa, onu anında sahneden sil!
            if (oppositeFragment != null)
            {
                Destroy(oppositeFragment);
            }

            // Kendimizi görünmez yapıp diyalogu başlatıyoruz
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            DialogueManager.instance.ShowDialogue(memoryLines, OnDialogueFinished);
        }
    }

    private void OnDialogueFinished()
    {
        PathBuilder builder = FindFirstObjectByType<PathBuilder>();

        if (builder != null)
        {
            builder.BuildPathSequentially(tilesToBuild);
        }

        Destroy(gameObject);
    }
}