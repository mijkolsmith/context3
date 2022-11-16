using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestActivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerArmature")
        {
            if (GameManager.Instance.QuestManager.currentQuest != GameManager.Instance.QuestManager.quests[1])
            {
                Debug.Log("speler!");
                GameManager.Instance.QuestManager.StartQuest(GameManager.Instance.QuestManager.quests[1]);
            }
        }
    }
}
