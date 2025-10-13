using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiverNPC : InteractableObject
{
    [Header("NPC Quest Setting")]
    public QuestData quesToGive;
    public string npmcName = "NPC";
    public string questStartMessage = "새로운 퀘스트가 있습니다.";
    public string noQuestMessage = "퀘스트가 없습니다.";
    public string QuestAlreadyActibeMessage = "이미 진행중인 퀘스트가 있습니다.";

    private QuestManager questManager;

    protected override void Start()
    {
        base.Start();
        questManager = FindObjectOfType<QuestManager>();

        if (questManager==null)
        {
            Debug.LogError("Quest가 없습니다.");
        }
        interactionText = "[E]" + npmcName + "와 대화하기";
    }

    public override void Interact()
    {
        base.Interact();

        questManager. StartQuest(quesToGive);
    }

    private void Update()
    {
        if (questManager !=null && questManager.currentQuest == null)
        {
            interactionText = "[E]" + npmcName;    
        }
    }


}
