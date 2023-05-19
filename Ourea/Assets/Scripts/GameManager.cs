using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField]
    PlayerController pC;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if(GameManager.Instance != null)
        {
            Destroy(gameObject);
            GameManager.Instance = this;
            
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UIManager.Instance.UpdateQuest("Movement", "Use ZQSD to move around");
    }



    public PlayerController GetPlayerController()
    {
        return pC;
    }
}
