using UnityEngine;

[CreateAssetMenu(fileName = "New Joker", menuName = "Scriptable Objects/Joker")]
public class JokerSO : ScriptableObject
{
    public enum JokerRarity
    {
        Common,
        Uncommon,
        Rare,
        Legendary
    }
    public JokerRarity rarity;
    public enum JokerEdition
    {
        Base,
        Foil,
        Holographic,
        Polychrome,
        Negative
    }
    public JokerEdition edition;
    public int chips;
    public float mult;
    public float multiplier;
}
