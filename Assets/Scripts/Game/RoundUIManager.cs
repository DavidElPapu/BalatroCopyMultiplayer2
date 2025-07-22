using UnityEngine;
using TMPro;

public class RoundUIManager : MonoBehaviour
{
    public TextMeshPro player1MoneyText, player2MoneyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayersMoneyUI(int player1Money, int player2Money)
    {
        player1MoneyText.text = player1Money.ToString();
        player2MoneyText.text = player2Money.ToString();
    }
}
