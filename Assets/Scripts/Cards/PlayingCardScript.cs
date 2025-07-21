using UnityEngine;

public class PlayingCardScript : MonoBehaviour
{
    public enum CardSuit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }
    public enum CardEnhancement
    {
        None,
        Glass,
        Gold,
        Lucky,
        Mult,
        Bonus,
        Stone,
        Steel,
        Wild
    }
    public enum CardEdition
    {
        Base,
        Foil,
        Holographic,
        Polychrome
    }
    public enum CardSeal
    {
        None,
        Red,
        Blue,
        Purple,
        Gold
    }

    //public 
    public int rank;
    public CardSuit suit;
    public CardEnhancement enhancement;
    public CardEdition edition;
    public CardSeal seal;

    [Header("Sprites")]
    public Sprite[] clubRankSprites = new Sprite[13];
    public Sprite[] diamondRankSprites = new Sprite[13];
    public Sprite[] heartRankSprites = new Sprite[13];
    public Sprite[] spadeRankSprites = new Sprite[13];
    public Sprite[] cardEnhancementSprites = new Sprite[9];
    public Sprite[] cardEditionSprites = new Sprite[3];
    public Sprite[] cardSealSprites = new Sprite[4];
    public SpriteRenderer cardSuitedRankSprite, cardEnhancementSprite, cardEditionSprite, cardSealSprite; 

    public void SetData(int rank, CardSuit suit, CardEnhancement enhancement, CardEdition edition, CardSeal seal)
    {
        this.rank = rank;
        ChangeSuit(suit);
        ChangeEnhancement(enhancement);
        ChangeEdition(edition);
        ChangeSeal(seal);
    }

    public void OnTrigger()
    {

    }

    public void OnDiscard()
    {

    }

    public void OnTriggerInHand()
    {

    }

    public void OnRoundEnd()
    {

    }

    public void OnCardDelete()
    {

    }

    public void ChangeRank(int newRank)
    {
        rank = newRank;
        switch (suit)
        {
            case CardSuit.Clubs:
                cardSuitedRankSprite.sprite = clubRankSprites[rank - 1];
                break;
            case CardSuit.Diamonds:
                cardSuitedRankSprite.sprite = diamondRankSprites[rank - 1];
                break;
            case CardSuit.Hearts:
                cardSuitedRankSprite.sprite = heartRankSprites[rank - 1];
                break;
            case CardSuit.Spades:
                cardSuitedRankSprite.sprite = spadeRankSprites[rank - 1];
                break;
            default:
                Debug.LogError("No tiene suit");
                break;
        }
    }

    public void ChangeSuit(CardSuit newSuit)
    {
        suit = newSuit;
        switch (suit)
        {
            case CardSuit.Clubs:
                cardSuitedRankSprite.sprite = clubRankSprites[rank - 1];
                break;
            case CardSuit.Diamonds:
                cardSuitedRankSprite.sprite = diamondRankSprites[rank - 1];
                break;
            case CardSuit.Hearts:
                cardSuitedRankSprite.sprite = heartRankSprites[rank - 1];
                break;
            case CardSuit.Spades:
                cardSuitedRankSprite.sprite = spadeRankSprites[rank - 1];
                break;
            default:
                Debug.LogError("Suit no valido");
                break;
        }
    }

    public void ChangeEnhancement(CardEnhancement newEnhancement)
    {
        enhancement = newEnhancement;
        switch (enhancement)
        {
            case CardEnhancement.None:
                cardEnhancementSprite.sprite = cardEditionSprites[0];
                break;
            case CardEnhancement.Glass:
                cardEnhancementSprite.sprite = cardEditionSprites[4];
                break;
            case CardEnhancement.Gold:
                cardEnhancementSprite.sprite = cardEditionSprites[7];
                break;
            case CardEnhancement.Lucky:
                cardEnhancementSprite.sprite = cardEditionSprites[8];
                break;
            case CardEnhancement.Mult:
                cardEnhancementSprite.sprite = cardEditionSprites[2];
                break;
            case CardEnhancement.Bonus:
                cardEnhancementSprite.sprite = cardEditionSprites[1];
                break;
            case CardEnhancement.Stone:
                cardEnhancementSprite.sprite = cardEditionSprites[6];
                break;
            case CardEnhancement.Steel:
                cardEnhancementSprite.sprite = cardEditionSprites[5];
                break;
            case CardEnhancement.Wild:
                cardEnhancementSprite.sprite = cardEditionSprites[3];
                break;
            default:
                Debug.LogError("Enhancement no valido");
                break;
        }
    }

    public void ChangeEdition(CardEdition newEdition)
    {
        edition = newEdition;
        switch (edition)
        {
            case CardEdition.Base:
                cardEditionSprite.sprite = null;
                break;
            case CardEdition.Foil:
                cardEditionSprite.sprite = cardEditionSprites[0];
                break;
            case CardEdition.Holographic:
                cardEditionSprite.sprite = cardEditionSprites[1];
                break;
            case CardEdition.Polychrome:
                cardEditionSprite.sprite = cardEditionSprites[2];
                break;
            default:
                Debug.LogError("Edition no valido");
                break;
        }
    }

    public void ChangeSeal(CardSeal newSeal)
    {
        seal = newSeal;
        switch (seal)
        {
            case CardSeal.None:
                cardSealSprite.sprite = null;
                break;
            case CardSeal.Red:
                cardSealSprite.sprite = cardSealSprites[1];
                break;
            case CardSeal.Blue:
                cardSealSprite.sprite = cardSealSprites[2];
                break;
            case CardSeal.Purple:
                cardSealSprite.sprite = cardSealSprites[3];
                break;
            case CardSeal.Gold:
                cardSealSprite.sprite = cardSealSprites[0];
                break;
            default:
                Debug.LogError("Seal no valido");
                break;
        }
    }
}
