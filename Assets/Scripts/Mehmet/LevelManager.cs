using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
     public static event UnityAction<bool> OnLevelLoaded;
     private const string _level = "Level ";
     private Scene _lastLoadedScene;
     public GameObject LoadingBar;
     private int currentLevel = 1;

     private void Awake() => LevelLoad();
     private void LevelLoad()
     {
          // currentLevel = PlayerData.playerData.currentLevel;
          if (LoadingBar != null) LoadingBar.SetActive(true);
          
           SceneLoader(currentLevel.ToString());
     }

     public void SetCurrentLevel()
     {
          currentLevel++;
          if (currentLevel >= SceneManager.sceneCountInBuildSettings)
               currentLevel = 1;
          SceneLoader(currentLevel.ToString());
     }

     public void SceneLoader(string name) => ChangeScene(name);
     void ChangeScene(string sceneName)
     {
          if (LoadingBar != null)
               LoadingBar.SetActive(true);
          StartCoroutine(SceneController(_level + sceneName));
     }

     IEnumerator SceneController(string sceneName)
     {
          OnLevelLoaded?.Invoke(false);

          if (_lastLoadedScene.IsValid())
          {
               SceneManager.UnloadSceneAsync(_lastLoadedScene);
               bool isUnloadScene = false;
               while (!isUnloadScene)
               {
                    isUnloadScene = !_lastLoadedScene.IsValid();
                    yield return new WaitForEndOfFrame();

               }
          }

          SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

          bool isSceneLoaded = false;

          while (!isSceneLoaded)
          {
               _lastLoadedScene = SceneManager.GetSceneByName(sceneName);
               isSceneLoaded = _lastLoadedScene != null && _lastLoadedScene.isLoaded;

               yield return new WaitForEndOfFrame();
          }

          OnLevelLoaded?.Invoke(true);
          if(LoadingBar != null) LoadingBar.SetActive(false);

     }
     public void NextLevel() => SetCurrentLevel();
     public void RestartLevel() => LevelLoad();
}
#if UNITY_EDITOR


[CustomEditor(typeof(LevelManager))]
public class LevelManagerCustom : Editor
{
     public override void OnInspectorGUI()
     {
          base.OnInspectorGUI();
          if (GUILayout.Button("Next Level"))
               LevelManager.Instance.NextLevel();
     }
}
#endif