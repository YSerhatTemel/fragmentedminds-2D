using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;    
    public TMP_Text dialogueText;     
    public float typingSpeed = 0.05f; 
    public bool isDialogueActive = false;

    public static DialogueManager instance; 


    private string[] currentLines;     // Okunacak tüm sayfaların listesi
    private int currentLineIndex;      // Şu an kaçıncı sayfadayız?
    private bool isTyping;             // Yazı daktilo gibi akmaya devam ediyor mu?
    private Coroutine typingCoroutine; // Çalışan daktilo işlemini tutan referans

    private void Awake()
    {
        instance = this;
        dialogueBox.SetActive(false); 
    }

    // Update, oyun çalıştığı sürece her saniye (frame) oyuncunun tuşlara basıp basmadığını dinler
    private void Update()
    {
        // Eğer diyalog kutusu ekranda yoksa, boşuna tuşları dinleme
        if (!dialogueBox.activeInHierarchy) return;

        // Oyuncu Boşluk (Space) veya E tuşuna basarsa...
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
        {
            if (isTyping)
            {
                // Eğer yazı hala harf harf akıyorsa: Bekletme, yazının tamamını anında ekrana bas (Skip)
                StopCoroutine(typingCoroutine);
                dialogueText.text = currentLines[currentLineIndex];
                isTyping = false;
            }
            else
            {
                // Eğer yazı çoktan bittiyse: Bir sonraki sayfaya geç
                NextLine();
            }
        }
    }

    // Anı parçasından tek bir mesaj yerine artık bir 'mesaj listesi' alıyoruz
    public void ShowDialogue(string[] lines)
    {
        isDialogueActive = true;
        currentLines = lines;
        currentLineIndex = 0; // İlk sayfadan başla
        dialogueBox.SetActive(true); 
        StartTyping();
    }

    private void StartTyping()
    {
        // Kutuyu temizleyip daktilo efektini başlatıyoruz
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(currentLines[currentLineIndex]));
    }

    private IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogueText.text = ""; 

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter; 
            yield return new WaitForSeconds(typingSpeed); 
        }

        isTyping = false; // Yazı bitti
    }

    private void NextLine()
    {
        currentLineIndex++; // Sayfa numarasını 1 artır

        if (currentLineIndex < currentLines.Length)
        {
            // Eğer okunacak başka sayfa varsa onu yazmaya başla
            StartTyping();
        }
        else
        {
            isDialogueActive = false;
            // Eğer tüm sayfalar bittiyse kutuyu tamamen kapat
            dialogueBox.SetActive(false);
        }
    }
}