using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject dialogueBox;    
    public TMP_Text dialogueText;     
    public GameObject continueIcon; 
    
    [Header("Settings")]
    public float typingSpeed = 0.05f; 

    [Header("Audio")]
    public AudioSource audioSource; 
    public AudioClip typingSound;   
    
    // YENİ: Sesin ne sıklıkla çalacağını Inspector'dan ayarlamamızı sağlayan değişken
    [Range(1, 5)]
    public int soundFrequency = 2; // Varsayılan olarak her 2 harfte 1 ses çalar

    public static DialogueManager instance; 
    public bool isDialogueActive = false; 

    private string[] currentLines;     
    private int currentLineIndex;      
    private bool isTyping;             
    private Coroutine typingCoroutine; 

    private void Awake()
    {
        instance = this;
        dialogueBox.SetActive(false); 
        continueIcon.SetActive(false); 
    }

    private void Update()
    {
        if (!dialogueBox.activeInHierarchy) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = currentLines[currentLineIndex];
                isTyping = false;
                continueIcon.SetActive(true); 
            }
            else
            {
                NextLine();
            }
        }
    }

    public void ShowDialogue(string[] lines)
    {
        isDialogueActive = true; 
        currentLines = lines;
        currentLineIndex = 0;
        dialogueBox.SetActive(true); 
        StartTyping();
    }

    private void StartTyping()
    {
        continueIcon.SetActive(false); 
        
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(currentLines[currentLineIndex]));
    }

    private IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogueText.text = ""; 

        // YENİ: Başlamadan önce kasetimizi hoparlöre takıyoruz
        if (audioSource != null && typingSound != null)
        {
            audioSource.clip = typingSound;
        }

        int charCount = 0; 

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter; 

            if (letter != ' ')
            {
                charCount++;
                
                if (charCount % soundFrequency == 0 && audioSource != null)
                {
                    // İŞTE BÜYÜK DEĞİŞİKLİK BURADA:
                    audioSource.Stop(); // 1. Varsa hala çalan eski sesi bıçak gibi kes!
                    audioSource.pitch = Random.Range(0.9f, 1.1f); 
                    audioSource.Play(); // 2. Temiz bir şekilde baştan çal (PlayOneShot yerine Play)
                }
            }

            yield return new WaitForSeconds(typingSpeed); 
        }

        isTyping = false; 
        continueIcon.SetActive(true); 
    }

    private void NextLine()
    {
        currentLineIndex++; 

        if (currentLineIndex < currentLines.Length)
        {
            StartTyping();
        }
        else
        {
            dialogueBox.SetActive(false);
            isDialogueActive = false; 
            continueIcon.SetActive(false); 
        }
    }
}