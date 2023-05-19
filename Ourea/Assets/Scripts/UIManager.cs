using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField]
    GameObject crosshair;

    [SerializeField]
    TextMeshProUGUI height;

    [SerializeField]
    PlayerController pc;

    [SerializeField]
    TextMeshProUGUI questTitleUI, questDescriptionUI;

    [SerializeField]
    Image[] slots;
    [SerializeField]
    Color defaultColor;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (UIManager.Instance != null)
        {
            Destroy(gameObject);
            UIManager.Instance = this;

        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        crosshair.SetActive(false);
    }

    private void Update()
    {
        height.text = " - " +  Mathf.Abs((int) pc.transform.position.y ).ToString();
        
    }

    public void SetCursorActive(bool isActive)
    {
        crosshair.SetActive(isActive);
    }


    public void UpdateQuest(string questTitle, string questDescription)
    {
        questTitleUI.text = questTitle; 
        questDescriptionUI.text = questDescription;
    }

    public void SelectSlot(int slotToActivate)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i == slotToActivate)
            {
                slots[i].color = Color.white;
            }
            else
            {

                slots[i].color = defaultColor;
                Debug.Log(slots[i]);
            }
        }
    }

    public void UnSelectSlot()
    {
        foreach (Image slot in slots)
        {
            slot.color = defaultColor;

        }
    }

}
