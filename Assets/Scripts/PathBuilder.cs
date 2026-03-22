using UnityEngine;
using UnityEngine.Tilemaps; // Required library to control the Tilemap system!

public class PathBuilder : MonoBehaviour
{
    [Header("Path Settings")]
    public Tilemap targetTilemap; // The Tilemap where the path will be drawn
    public TileBase baseTile; // <--- İŞTE DEĞİŞEN TEK KELİME: "Tile" yerine "TileBase" oldu.

    // This function will be called when a memory is read (after dialogue ends)
    public void BuildSingleTile(Vector3Int tilePosition)
    {
        // Places the path tile at the specified coordinates
        targetTilemap.SetTile(tilePosition, baseTile);
    }
}