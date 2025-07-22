using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class CardCreator : NetworkBehaviour
{
    public static CardCreator singleton;

    public GameObject cardPrefab;
    [Header("Sprites")]
    public Sprite[] clubRankSprites = new Sprite[13];
    public Sprite[] diamondRankSprites = new Sprite[13];
    public Sprite[] heartRankSprites = new Sprite[13];
    public Sprite[] spadeRankSprites = new Sprite[13];
    public Sprite[] cardEnhancementSprites = new Sprite[9];
    public Sprite[] cardEditionSprites = new Sprite[3];
    public Sprite[] cardSealSprites = new Sprite[4];

    private void Awake()
    {
        if (singleton != null && singleton != this) { Destroy(this); } else { singleton = this; }
    }

    public PlayingCardScript CreateRandomCard(bool canHaveExtras, Transform pos)
    {
        GameObject newCard = Instantiate(cardPrefab, pos.position, pos.rotation);
        if (canHaveExtras)
            newCard.GetComponent<PlayingCardScript>().SetData(GetRandomRank(), GetRandomSuit(), GetRandomEnhancement(), GetRandomEdition(), GetRandomSeal());
        else
            newCard.GetComponent<PlayingCardScript>().SetData(GetRandomRank(), GetRandomSuit(), CardEnhancements.None, CardEditions.Base, CardSeals.None);
        return newCard.GetComponent<PlayingCardScript>();
    }

    public GameObject CreateCardWithData(int rank, CardSuits suit, CardEnhancements enhancement, CardEditions edition, CardSeals seal, Transform spawnTransform)
    {
        GameObject newCard = Instantiate(cardPrefab, spawnTransform.position, spawnTransform.rotation);
        newCard.GetComponent<PlayingCardScript>().SetData(rank, suit, enhancement, edition, seal);
        return newCard;
    }

    public CardData GetRandomCardData()
    {
        return new CardData(GetRandomRank(), GetRandomSuit(), GetRandomEnhancement(), GetRandomEdition(), GetRandomSeal());
    }

    #region Random Card Data Generator

    private int GetRandomRank()
    {
        return Random.Range(1, 14);
    }

    private CardSuits GetRandomSuit()
    {
        int randomIndex = Random.Range(1, 5);
        switch (randomIndex)
        {
            case 1:
                return CardSuits.Clubs;
            case 2:
                return CardSuits.Diamonds;
            case 3:
                return CardSuits.Hearts;
            case 4:
                return CardSuits.Spades;
            default:
                Debug.Log("Random dio un valor no valido, se pondra diamante de mientras");
                return CardSuits.Diamonds;
        }
    }

    private CardEnhancements GetRandomEnhancement()
    {
        //por ahora, la probabilidad de que salga sin enhancement es 60%
        int randomIndex = Random.Range(1, 21);
        switch (randomIndex)
        {
            case 1:
                return CardEnhancements.Bonus;
            case 2:
                return CardEnhancements.Mult;
            case 3:
                return CardEnhancements.Wild;
            case 4:
                return CardEnhancements.Glass;
            case 5:
                return CardEnhancements.Steel;
            case 6:
                return CardEnhancements.Stone;
            case 7:
                return CardEnhancements.Gold;
            case 8:
                return CardEnhancements.Lucky;
            default:
                return CardEnhancements.None;
        }
    }

    private CardEditions GetRandomEdition()
    {
        //por ahora la probabilidad de que salga poly es 1.2%, holo 2.8% y foil 4%
        float randomIndex = Random.Range(0, 100f);
        if (randomIndex <= 1.2f)
            return CardEditions.Polychrome;
        else if (randomIndex <= 4f)
            return CardEditions.Holographic;
        else if (randomIndex <= 8f)
            return CardEditions.Foil;
        else
            return CardEditions.Base;
    }

    private CardSeals GetRandomSeal()
    {
        //por ahora la probabilidad de que salga sin seal es de 80%
        int randomIndex = Random.Range(1, 21);
        switch (randomIndex)
        {
            case 1:
                return CardSeals.Gold;
            case 2:
                return CardSeals.Red;
            case 3:
                return CardSeals.Blue;
            case 4:
                return CardSeals.Purple;
            default:
                return CardSeals.None;
        }
    }

    #endregion
}
public struct CardData
{
    public int rank;
    public CardSuits suit;
    public CardEnhancements enhancement;
    public CardEditions edition;
    public CardSeals seal;

    public CardData(int rank, CardSuits suit, CardEnhancements enhancement, CardEditions edition, CardSeals seal)
    {
        this.rank = rank;
        this.suit = suit;
        this.enhancement = enhancement;
        this.edition = edition;
        this.seal = seal;
    }
}

public enum CardSuits
{
    Clubs,
    Diamonds,
    Hearts,
    Spades
}

public enum CardEnhancements
{
    None,
    Bonus,
    Mult,
    Wild,
    Glass,
    Steel,
    Stone,
    Gold,
    Lucky
}

public enum CardEditions
{
    Base,
    Foil,
    Holographic,
    Polychrome
}
public enum CardSeals
{
    None,
    Gold,
    Red,
    Blue,
    Purple
}
