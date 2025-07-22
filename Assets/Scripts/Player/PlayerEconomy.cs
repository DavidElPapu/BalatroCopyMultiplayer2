using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.BouncyCastle.Utilities.IO;
using TMPro;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/guides/networkbehaviour
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

public class PlayerEconomy : NetworkBehaviour
{
    public static PlayerEconomy singleton;

    public int startingMoney;
    public TextMeshPro player1MoneyText, player2MoneyText;
    private List<MainPlayerScript> players = new List<MainPlayerScript>();
    private List<int> playersMoney = new List<int>();

    private void Awake()
    {
        if (singleton != null && singleton != this) { Destroy(this); } else { singleton = this; }
    }

    public void RegisterPlayer(MainPlayerScript player)
    {
        players.Add(player);
        playersMoney.Add(startingMoney);
        //ChangePlayerMoney(player, startingMoney);
        Debug.Log("Se registro el jugador #" + player.name);
    }

    public int GetPlayerMoney(MainPlayerScript player)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == player)
                return playersMoney[i];
        }
        Debug.LogError("No se encontro jugador");
        return 0;
    }

    public void ChangePlayerMoney(MainPlayerScript player, int moneyChange)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == player)
            {
                playersMoney[i] += moneyChange;
                //player.CommandUpdateMoneyUI();
                return;
            }
        }
        Debug.LogError("No se encontro jugador");
    }

    public void UpdateMoneyUI(int player1Money, int player2Money)
    {
        player1MoneyText.text = player1Money.ToString();
        player2MoneyText.text = player2Money.ToString();
    }

    public int GetPlayers()
    {
        return players.Count;
    }

    public int GetPlayerMoney(int playerIndex)
    {
        return playersMoney[playerIndex];
    }

    private void UpdateMoneyText(int playerIndex)
    {
        switch (playerIndex)
        {
            case 0:
                player1MoneyText.text = playersMoney[playerIndex].ToString();
                break;
            case 1:
                player2MoneyText.text = playersMoney[playerIndex].ToString();
                break;
            default:
                break;
        }
    }
}
