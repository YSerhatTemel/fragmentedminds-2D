using System.Collections;
using System; // Action kullanabilmek için bu kütüphane şart
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
    
    [Range(1, 5)]
    public int soundFrequency = 2; 

    public static DialogueManager instance; 
    public bool isDialogueActive = false; 

    private string[] currentLines;     
    private int currentLineIndex;      
    private bool isTyping;             
    private Coroutine typingCoroutine; 
    
    // Diyalog bittiğinde çalıştırılacak fonksiyonu burada tutacağız
    private Action onDialogueEndCallback; 

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

    // Parametreye isteğe bağlı bir 'Action' (onComplete) ekledik
    public void ShowDialogue(string[] lines, Action onComplete = null)
    {
        isDialogueActive = true; 
        currentLines = lines;
        currentLineIndex = 0;
        
        onDialogueEndCallback = onComplete; // Bize verilen görevi hafızaya alıyoruz
        
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
                    audioSource.Stop(); 
                    audioSource.pitch = 1.0f; 
                    audioSource.Play(); 
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
            // DİYALOG TAMAMEN BİTTİ!
            dialogueBox.SetActive(false);
            isDialogueActive = false; 
            continueIcon.SetActive(false); 
            
            // Eğer bize verilen bir "bitiş görevi" varsa, onu tetikliyoruz
            if (onDialogueEndCallback != null)
            {
                onDialogueEndCallback.Invoke(); // MemoryFragment'taki fonksiyonu çalıştırır
                onDialogueEndCallback = null; // Görev bitti, hafızayı temizle
            }
        }
    }
}