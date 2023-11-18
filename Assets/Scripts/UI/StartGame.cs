using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void GameStart()
    {
        Time.timeScale = 1;
        GameEvent.instance.gameStarted.SetValueAndForceNotify(true);
    }
}
