using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    private string playerName;
    private int maxPlayers;

    public void SetPlayerName(string nameText)
    {
        playerName = nameText;
    }

    public void SetMaxPlayers(int number)
    {
        maxPlayers = number;
    }

    public string GetPlayerName()
    {
        if (string.IsNullOrEmpty(playerName))
            return RandomName();
        else
            return playerName;
    }

    public int GetMaxPlayers()
    {
        if (maxPlayers != 2 && maxPlayers != 4)
            return 2;
        else
            return maxPlayers;
    }

    private string RandomName()
    {
        string a = "Balatrero #";
        string b = Random.Range(1, 457).ToString();
        return $"{a}{b}";
    }
}
