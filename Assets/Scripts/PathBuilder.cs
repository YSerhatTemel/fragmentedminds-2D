using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathBuilder : MonoBehaviour
{
    [Header("Path Settings")]
    public Tilemap targetTilemap;
    public TileBase baseTile;
    public float timeBetweenTiles = 0.2f;

    [Header("Crumbling Mind Mechanics")]
    [Tooltip("Should this path collapse after being built?")]
    public bool shouldCollapse = true; 
    
    [Tooltip("How many seconds to wait before the path starts collapsing?")]
    public float collapseWaitTime = 4.0f; // Oyuncuya eşyaları toplaması için verdiğimiz süre
    
    [Tooltip("How fast should the tiles disappear?")]
    public float collapseSpeed = 0.3f; // Taşların arkadan silinme hızı

    public void BuildPathSequentially(Vector3Int[] tiles)
    {
        StartCoroutine(BuildRoutine(tiles));
    }

    private IEnumerator BuildRoutine(Vector3Int[] tiles)
    {
        // 1. Yolu sırayla inşa et
        foreach (Vector3Int pos in tiles)
        {
            targetTilemap.SetTile(pos, baseTile);
            yield return new WaitForSeconds(timeBetweenTiles);
        }

        // 2. Eğer çökme mekaniği açıksa, yıkım sürecini başlat!
        if (shouldCollapse)
        {
            StartCoroutine(CollapseRoutine(tiles));
        }
    }

    private IEnumerator CollapseRoutine(Vector3Int[] tiles)
    {
        // Oyuncuya eşyaları fark edip koşmaya başlaması için süre veriyoruz
        yield return new WaitForSeconds(collapseWaitTime);

        Debug.Log("The mind is rejecting the memory. Path is collapsing!");

        // Taşları baştan sona (oyuncunun arkasından başlayarak) tek tek sil
        foreach (Vector3Int pos in tiles)
        {
            targetTilemap.SetTile(pos, null); // Taşı karanlığa göm
            yield return new WaitForSeconds(collapseSpeed);
        }
    }
}