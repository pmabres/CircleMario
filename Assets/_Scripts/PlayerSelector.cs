using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour
{
    public Image LuigiPanel;
    public Image MarioPanel;
    private void SelectPlayer(GameManager.Player selectPlayer)
    {
        GameManager.SelectedPlayer = selectPlayer;
    }

    public void SelectLuigi()
    {
        SelectPlayer(GameManager.Player.Luigi);
        LuigiPanel.color = new Color(1, 1, 1, 0.8f);
        MarioPanel.color = new Color(1, 1, 1, 0.2f);
    }

    public void SelectMario()
    {
        SelectPlayer(GameManager.Player.Mario);
        LuigiPanel.color = new Color(1, 1, 1, 0.2f);
        MarioPanel.color = new Color(1, 1, 1, 0.8f);
    }
}
