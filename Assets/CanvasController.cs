using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : Singleton<CanvasController>
{
     public GameObject StartPanel, FinishPanel, RunningPanel;
     public GameObject Camera;

     private void Awake()
     {
          StartPanel.SetActive(true);
     }


     public void RunGame()
     {
          Camera.SetActive(false);
          StartPanel.SetActive(false);
          FinishPanel.SetActive(false);
          RunningPanel.SetActive(true);
          UIManager.Instance.isGameStart = true;
     }

     public void QuitGame()
     {
          Application.Quit();
     }


     public void FinishGame()
     {
          //  Camera.SetActive(true);
          StartPanel.SetActive(false);
          FinishPanel.SetActive(true);
          RunningPanel.SetActive(false);
          UIManager.Instance.isGameStart = false;
     }

     public void StartGame()
     {
          UIManager.Instance.isGameStart = false;
          UIManager.Instance.Initialize();
          LevelManager.Instance.RestartLevel();
     }

}
