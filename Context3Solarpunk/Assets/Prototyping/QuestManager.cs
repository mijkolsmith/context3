using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [HideInInspector] public Quest currentQuest;
    private int questNmbr;
    public List<Quest> quests = new List<Quest>();

    private void Start()
    {

    }
}
