using System.Collections; // Zamanlayıcı (Coroutine) kullanmak için bu şart
using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    [TextArea(3, 5)] 
    public string[] memoryLines; 

    [Header("Path Building Data")]
    public Vector3Int[] tilesToBuild; 

    [Header("Choice Mechanics")]
    [Tooltip("The rival fragment that will be DESTROYED if this one is chosen")]
    public GameObject oppositeFragment; 

    [Header("Reveal Items")]
    [Tooltip("Drag the memory pieces here that should APPEAR when this path is chosen")]
    public GameObject[] itemsToReveal; 
    
    [Tooltip("Yol inşa edilmeye başladıktan kaç saniye sonra eşyalar belirecek?")]
    public float itemRevealDelay = 1.5f; // YENİ: Bekleme süresi ayarı

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // 1. Rakibi anında yok et
            if (oppositeFragment != null)
            {
                Destroy(oppositeFragment);
            }

            // DİKKAT: Eşyaları artık burada görünür yapmıyoruz! Bekleyeceğiz.

            // 2. Kendimizi görünmez yapıp diyalogu başlatıyoruz
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            DialogueManager.instance.ShowDialogue(memoryLines, OnDialogueFinished);
        }
    }

    private void OnDialogueFinished()
    {
        // Diyalog bitince normal fonksiyon yerine zamanlayıcılı fonksiyonumuzu başlatıyoruz
        StartCoroutine(BuildPathAndWait());
    }

    private IEnumerator BuildPathAndWait()
    {
        // 1. Yol inşa etme emrini ver
        PathBuilder builder = FindFirstObjectByType<PathBuilder>();
        if (builder != null)
        {
            builder.BuildPathSequentially(tilesToBuild);
        }

        // 2. Belirlediğimiz süre boyunca (örneğin 1.5 saniye) bekle
        yield return new WaitForSeconds(itemRevealDelay);

        // 3. Süre doldu! Şimdi o gizli anı parçalarını şak diye yolda belirmesini sağla
        foreach (GameObject item in itemsToReveal)
        {
            if (item != null) item.SetActive(true);
        }

        // İşimiz tamamen bitti, artık bu objeyi (Sarı Anıyı) silebiliriz
        Destroy(gameObject);
    }
}