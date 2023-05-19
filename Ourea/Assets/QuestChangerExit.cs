using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChangerExit : MonoBehaviour
{
    public string title;
    public string description;

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            UIManager.Instance.UpdateQuest(title, description);
        }
    }
}
