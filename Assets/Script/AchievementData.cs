using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement" , menuName = "Achievement/Achevement Data")]

public class AchievementData : ScriptableObject
{
    // Start is called before the first frame update
    public string achievementName;
    public string description;
    public AchievementType achievementType;
    public int requiredAmount;
    public int rewardCoins;
    public bool isUnlocked;
    public Sprite icon;
}
