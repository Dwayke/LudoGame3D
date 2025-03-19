using System.Collections.Generic;
using UnityEngine;

public class LudoBoardManager : MonoBehaviour
{
    #region VARS
    [SerializeField] GameObject _board;
    public List<LudoTile> waypointTileList;
    public List<LudoTile> baseTileList;
    #endregion
    #region ENGINE
    private void OnEnable()
    {
        LudoManagers.Instance.PlayerController.OnPieceSelected += OnPieceSelected;
    }
    private void OnDisable()
    {
        LudoManagers.Instance.PlayerController.OnPieceSelected -= OnPieceSelected;
    }
    #endregion
    #region MEMBER METHODS
    public void AddTile(LudoTile tile)
    {
        if(tile.type == ETileType.Waypoint) waypointTileList.Add(tile); 
        if(tile.type == ETileType.Base) baseTileList.Add(tile); 
    }
    #endregion
    #region LOCAL METHODS
    private void OnPieceSelected(BaseDisk obj)
    {

    }
    #endregion
}
