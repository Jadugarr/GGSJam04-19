using GameEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterTypes
{
    Human,
    Planet,
    Galaxy
}


public class CharacterSelectButton : MonoBehaviour
{
    
    [SerializeField]private Button characterButton;
    [SerializeField]private CharacterTypes characterType;

    private void Awake()
    {
        characterButton.onClick.AddListener(CharacterButtonClicked);
    }

    private void CharacterButtonClicked()
    {
        EventManager.CallEvent(GameEvent.CharacterSelected,null);
        //set character type for sprite
    }
}
