using UnityEngine;

[CreateAssetMenu(fileName = "New Blind", menuName = "Scriptable Objects/Blind")]
public class BlindSO : ScriptableObject
{
    public string blindName;
    public string description;
    public Sprite icon;
    public float baseScoreMultiplier;
}
