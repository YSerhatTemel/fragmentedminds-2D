using System.Collections; // Coroutine için gerekli
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathBuilder : MonoBehaviour
{
    [Header("Path Settings")]
    public Tilemap targetTilemap; 
    public TileBase baseTile; 

    [Header("Animation Settings")]
    public float timeBetweenTiles = 0.2f; // Taşların belirme hızı (saniye)

    // MemoryFragment artık bu fonksiyonu çağıracak
    public void BuildPathSequentially(Vector3Int[] tilePositions)
    {
        StartCoroutine(PlaceTilesWithDelay(tilePositions));
    }

    // Taşları bekleyerek tek tek koyan sihirli fonksiyon
    private IEnumerator PlaceTilesWithDelay(Vector3Int[] tilePositions)
    {
        foreach (Vector3Int pos in tilePositions)
        {
            targetTilemap.SetTile(pos, baseTile);
            yield return new WaitForSeconds(timeBetweenTiles); // Belirlediğimiz süre kadar bekle
        }
    }
}