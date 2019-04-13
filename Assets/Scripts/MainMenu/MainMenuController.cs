using GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    //button
    public void Awake()
    {
        EventManager.AddListener(GameEvent.CharacterSelected, OnCharacterSelected);
    }

    public void OnCharacterSelected(IGameEvent eventParameters)
    {
        SceneManager.LoadScene("Game");
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(GameEvent.CharacterSelected, OnCharacterSelected);
    }


}
