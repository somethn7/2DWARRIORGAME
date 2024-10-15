using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public GameObject loadingScreen;

    public void NextLevelButton()
    {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            
            loadingScreen.SetActive(true);

          
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Sonraki seviyeyi yüklemek mümkün değil. Son seviyedesiniz.");
        }
    }

    
    public void MenuButton()
    {
       
        SceneManager.LoadScene(0);
    }

  
    public void QuitButton()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

