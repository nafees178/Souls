using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenLoadMenuInputManager : MonoBehaviour
{
    PlayerControls playerControls;

    [Header("Title Scren Inputs")]
    [SerializeField] bool deleteCharacterSlot = false;

    private void Update()
    {
        if (deleteCharacterSlot)
        {
            deleteCharacterSlot = false;
            TitleScreenManager.instance.AttemptToDeleteCharacterSlots();
        }
    }

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls= new PlayerControls();
            playerControls.UI.Delete.performed += I => deleteCharacterSlot = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
