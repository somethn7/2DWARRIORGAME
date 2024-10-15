using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Entry : MonoBehaviour
{
    public GameObject loadingScreen; 
    public Slider progressBar; 
    public float preloadDuration = 1f;

    [SerializeField] private AudioManage audioManager;
    [SerializeField] private string entrysound;
    [SerializeField] private Animator anim;
 
    public void StartButton()
    {
        StartCoroutine(LoadSceneAsync(1));
    }

   public void QuitButton()
{
    #if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
    #else
    Application.Quit();
    #endif
}

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        audioManager.Play(entrysound);
        
        float startTime = Time.time;
        while (Time.time < startTime + preloadDuration)
        {
            float elapsed = (Time.time - startTime) / preloadDuration;
            progressBar.value = Mathf.Lerp(0f, 1f, elapsed); 
            yield return null;
        }
        
        progressBar.value = 1f;
        anim.SetTrigger("Transistion");
        yield return new WaitForSeconds(1.3f);
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        
        while (!asyncOperation.isDone)
        {
            progressBar.value = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            yield return null;
        }
        
        loadingScreen.SetActive(false);
    }
}