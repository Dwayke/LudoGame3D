using System;
using System.Collections.Generic;
using UnityEngine;

public class LudoTurnManager : MonoBehaviour
{
    #region VARS
    public ETeam currentTurn = 0;
    public List<int> lastRolls;
    public event Action<ETeam> OnTurnSwitched;
    #endregion
    #region ENGINE
    private void Update()
    {
        LudoManagers.Instance.UIManager.DisplayCurrentTurn(currentTurn);
    }
    private void OnEnable()
    {
        LudoManagers.Instance.GameManager.OnDiceRollComplete += OnDiceRollComplete;
    }
    private void OnDisable()
    {
        if (LudoManagers.Instance != null)
        {
            LudoManagers.Instance.GameManager.OnDiceRollComplete -= OnDiceRollComplete;
        }
    }
    #endregion
    #region MEMBER METHODS
    public void SwitchTurn()
    {
        LudoManagers.Instance.GameStateManager.InitiateRollState();
        if (currentTurn != ETeam.Green) currentTurn += 1;
        else currentTurn = 0;
        OnTurnSwitched.Invoke(currentTurn);
    }
    #endregion
    #region LOCAL METHODS
    private void OnDiceRollComplete(int lastRoll, ETeam lastPlayer)
    {
        lastRolls.Add(lastRoll);
        if (!LudoManagers.Instance.GameManager.isGameStarted&&lastRoll!=6)
        {
            SwitchTurn();
        }
        else if (!CheckFreeDisks() && lastRoll != 6)
        {
            SwitchTurn();
        }
        Debug.Log("Last Move: "+lastRolls[^1]);
    }
    public bool CheckFreeDisks()
    {
        foreach (var disk in LudoManagers.Instance.BoardManager.availableDisks)
        {
            if (disk.diskState == EDiskState.Free && disk.pathIndex + LudoManagers.Instance.TurnManager.lastRolls[^1] <= 57) return true;
        }
        return false;
    }
    #endregion
}
