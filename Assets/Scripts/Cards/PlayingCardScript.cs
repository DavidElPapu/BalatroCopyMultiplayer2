using UnityEngine;

public class PlayingCardScript : MonoBehaviour
{
    //public 
    public int rank;
    public CardSuits suit;
    public CardEnhancements enhancement;
    public CardEditions edition;
    public CardSeals seal;

    
    public SpriteRenderer cardSuitedRankSprite, cardEnhancementSprite, cardEditionSprite, cardSealSprite; 

    public void SetData(int rank, CardSuits suit, CardEnhancements enhancement, CardEditions edition, CardSeals seal)
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
            case CardSuits.Clubs:
                cardSuitedRankSprite.sprite = CardCreator.singleton.clubRankSprites[rank - 1];
                break;
            case CardSuits.Diamonds:
                cardSuitedRankSprite.sprite = CardCreator.singleton.diamondRankSprites[rank - 1];
                break;
            case CardSuits.Hearts:
                cardSuitedRankSprite.sprite = CardCreator.singleton.heartRankSprites[rank - 1];
                break;
            case CardSuits.Spades:
                cardSuitedRankSprite.sprite = CardCreator.singleton.spadeRankSprites[rank - 1];
                break;
            default:
                Debug.LogError("No tiene suit");
                break;
        }
    }

    public void ChangeSuit(CardSuits newSuit)
    {
        suit = newSuit;
        switch (suit)
        {
            case CardSuits.Clubs:
                cardSuitedRankSprite.sprite = CardCreator.singleton.clubRankSprites[rank - 1];
                break;
            case CardSuits.Diamonds:
                cardSuitedRankSprite.sprite = CardCreator.singleton.diamondRankSprites[rank - 1];
                break;
            case CardSuits.Hearts:
                cardSuitedRankSprite.sprite = CardCreator.singleton.heartRankSprites[rank - 1];
                break;
            case CardSuits.Spades:
                cardSuitedRankSprite.sprite = CardCreator.singleton.spadeRankSprites[rank - 1];
                break;
            default:
                Debug.LogError("Suit no valido");
                break;
        }
    }

    public void ChangeEnhancement(CardEnhancements newEnhancement)
    {
        enhancement = newEnhancement;
        switch (enhancement)
        {
            case CardEnhancements.None:
                cardEnhancementSprite.sprite = CardCreator.singleton.cardEditionSprites[0];
                break;
            case CardEnhancements.Bonus:
                cardEnhancementSprite.sprite = CardCreator.singleton.cardEditionSprites[1];
                break;
            case CardEnhancements.Mult:
                cardEnhancementSprite.sprite = CardCreator.singleton.cardEditionSprites[2];
                break;
            case CardEnhancements.Wild:
                cardEnhancementSprite.sprite = CardCreator.singleton.cardEditionSprites[3];
                break;
            case CardEnhancements.Glass:
                cardEnhancementSprite.sprite = CardCreator.singleton.cardEditionSprites[4];
                break;
            case CardEnhancements.Steel:
                cardEnhancementSprite.sprite = CardCreator.singleton.cardEditionSprites[5];
                break;
            case CardEnhancements.Stone:
                cardEnhancementSprite.sprite = CardCreator.singleton.cardEditionSprites[6];
                break;
            case CardEnhancements.Gold:
                cardEnhancementSprite.sprite = CardCreator.singleton.cardEditionSprites[7];
                break;
            case CardEnhancements.Lucky:
                cardEnhancementSprite.sprite = CardCreator.singleton.cardEditionSprites[8];
                break;
            default:
                Debug.LogError("Enhancement no valido");
                break;
        }
    }

    public void ChangeEdition(CardEditions newEdition)
    {
        edition = newEdition;
        switch (edition)
        {
            case CardEditions.Base:
                cardEditionSprite.sprite = null;
                break;
            case CardEditions.Foil:
                cardEditionSprite.sprite = CardCreator.singleton.cardEditionSprites[0];
                break;
            case CardEditions.Holographic:
                cardEditionSprite.sprite = CardCreator.singleton.cardEditionSprites[1];
                break;
            case CardEditions.Polychrome:
                cardEditionSprite.sprite = CardCreator.singleton.cardEditionSprites[2];
                break;
            default:
                Debug.LogError("Edition no valido");
                break;
        }
    }

    public void ChangeSeal(CardSeals newSeal)
    {
        seal = newSeal;
        switch (seal)
        {
            case CardSeals.None:
                cardSealSprite.sprite = null;
                break;
            case CardSeals.Red:
                cardSealSprite.sprite = CardCreator.singleton.cardSealSprites[1];
                break;
            case CardSeals.Blue:
                cardSealSprite.sprite = CardCreator.singleton.cardSealSprites[2];
                break;
            case CardSeals.Purple:
                cardSealSprite.sprite = CardCreator.singleton.cardSealSprites[3];
                break;
            case CardSeals.Gold:
                cardSealSprite.sprite = CardCreator.singleton.cardSealSprites[0];
                break;
            default:
                Debug.LogError("Seal no valido");
                break;
        }
    }
}
