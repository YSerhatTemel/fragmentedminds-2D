using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    private TMP_Text textComponent;
    public float blinkSpeed = 2f; // Yanıp sönme hızı

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    void Update()
    {
        // Yazının saydamlık (Alpha) değerini zamanla 0 ve 1 arasında dalgalandırır
        Color c = textComponent.color;
        c.a = Mathf.PingPong(Time.time * blinkSpeed, 1f);
        textComponent.color = c;
    }
}