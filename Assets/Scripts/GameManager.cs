using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Collection System")]
    public int totalPiecesNeeded = 3; 
    public int currentPieces = 0;     

    [HideInInspector]
    public bool isGatewayOpen = false; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMemoryPiece()
    {
        currentPieces++;
        Debug.Log("Toplanan Parça: " + currentPieces + "/" + totalPiecesNeeded);

        if (currentPieces >= totalPiecesNeeded)
        {
            isGatewayOpen = true;
            Debug.Log("Tüm parçalar bulundu! Kapı açıldı.");
        }
    }

    public void ResetCollection()
    {
        currentPieces = 0;
        isGatewayOpen = false;
    }
}