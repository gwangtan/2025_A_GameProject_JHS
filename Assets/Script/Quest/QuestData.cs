using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]

public class QuestData : ScriptableObject
{
    [Header("기본정보")]
    public string questTitle = "새로운 퀘스트";
    [TextArea(2, 4)]
    public string description = "퀘스트 설명을 입력하시오";
    public Sprite questicon;

    [Header("퀘스트 상점")]
    public QuestType questType;
    public int targetAmount = 1;

    [Header("배달 퀘스트용 (Delivery)")]
    public Vector3 deliveryPosition;
    public float delivertRedius = 3f;

    [Header("수집/상호작용  퀘스트용")]
    public string targetTag = "";


    [Header("보상")]
    public int experienceReward = 100;
    public string rewardMessege = "퀘스트 완료";

    [Header("퀘스트 연결")]
    public QuestData newQuest;


    //런타임 데이터 (저장되지않음)
    [System.NonSerialized] public int currentProgress = 0;
    [System.NonSerialized] public bool isActive = false;
    [System.NonSerialized] public bool isCompleted = false;

    //퀘스트 초기화
    public void Initialize()
    {
        currentProgress = 0;
        isActive = false;
        isCompleted = false;
    }

    public bool IsComplete()
    {
        switch (questType)
        {
            case QuestType .Delivery:
                return currentProgress >= 1;
            case QuestType.Collect:
                case QuestType.Interact:
                return currentProgress >= targetAmount;
                default:
                return false;
                

        }
        

    }

    public float GetProgressPercentage()
    {
        if(targetAmount <= 0) return 0f;
        return Mathf.Clamp01((float)currentProgress / targetAmount);

    }

    public string GetProgressText()
    {
        switch(questType)
        {
            case QuestType .Delivery:
                return isCompleted ? "배달완료!" : "목적지로 이동하세요";
                case QuestType .Collect:
                return $"{currentProgress}/{targetAmount}";
                case QuestType .Interact:
                return $"{currentProgress}/{targetAmount}";
            default:
                return "";
        }
    }

}
