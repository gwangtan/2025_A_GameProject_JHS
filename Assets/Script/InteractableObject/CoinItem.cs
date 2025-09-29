using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : InteractableObject
{
    [Header("���� ����")]
    public int coinValue = 10;
    public string questTag = "Coin";

    protected override void Start()
    {
        base.Start();
        objectName = "����";
        interactionText = "[E]����ȹ��";
        interactionType = InteractionType.Item;
    }

    protected override void CollectItem()
    {
        if(QuestManager.Instance != null)
        {
            QuestManager.Instance.AddCollectProgress(questTag);
        }


        transform.Rotate(Vector3.up * 180f);
        Destroy(gameObject , 0.5f);
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}
