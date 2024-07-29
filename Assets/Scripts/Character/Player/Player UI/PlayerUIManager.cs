using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;
    [Header("Network Join")]
    [SerializeField] bool startGameAsClient;

    [HideInInspector] public PlayerUIHUDManager playerUIHUDManager;
    [HideInInspector] public PlayerUIPopUpManager PlayerUIPopUpManager;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerUIHUDManager = GetComponentInChildren<PlayerUIHUDManager>();
        PlayerUIPopUpManager = GetComponentInChildren<PlayerUIPopUpManager>();
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (startGameAsClient)
        {
            startGameAsClient = false;
            NetworkManager.Singleton.Shutdown();
            NetworkManager.Singleton.StartClient();
        }
    }
}
