using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject loadingScreen; 
    [SerializeField] private Animator anim; 

    public void RetryButton()
    {
        StartCoroutine(RetryGameAsync());
    }

    private IEnumerator RetryGameAsync()
    {
        loadingScreen.SetActive(true);
        

      
        yield return new WaitForSeconds(0.5f);

       
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);

      
        while (!asyncOperation.isDone)
        {
         
            yield return null;
        }

      
        loadingScreen.SetActive(false);
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