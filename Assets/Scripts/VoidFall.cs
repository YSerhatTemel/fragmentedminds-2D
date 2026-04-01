using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement; // Sahneyi yeniden başlatmak için gerekli

public class VoidFall : MonoBehaviour
{
    [Header("Abyss Settings")]
    [Tooltip("Drag the Tilemap that holds the paths here")]
    public Tilemap pathTilemap;

    private void Update()
    {
        // Eğer Tilemap atanmadıysa hata vermemesi için bekle
        if (pathTilemap == null) return;

        // Karakterin dünyadaki gerçek pozisyonunu, Tilemap'in kare (grid) sistemine çevirir
        Vector3Int currentCell = pathTilemap.WorldToCell(transform.position);

        // Eğer karakterin ayak bastığı o karede bir taş (tile) YOKSA...
        if (!pathTilemap.HasTile(currentCell))
        {
            FallIntoAbyss();
        }
    }

    private void FallIntoAbyss()
    {
        Debug.Log("Player fell into the abyss! Resetting memory...");
        
        // 1. Toplanan parça sayısını GameManager'dan sıfırla ki hile olmasın
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetCollection();
        }
        
        // 2. Sahneyi baştan yükle (Hafıza başa sarar)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}