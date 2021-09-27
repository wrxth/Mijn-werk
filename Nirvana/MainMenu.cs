using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    List<AsyncOperation> ScenesLoading = new List<AsyncOperation>();
    [SerializeField] private GameObject Menu;

    [SerializeField] private GameObject ProgressInterface;

    [SerializeField] private Image ProgressBar;

    public void Play()
    {
        Menu.SetActive(false);
        ProgressInterface.SetActive(true);

        ScenesLoading.Add(SceneManager.LoadSceneAsync("Game"));
        
        StartCoroutine(LoadingScreen());
    }

    private IEnumerator LoadingScreen()
    {
        float progress = 0;

        for (int i = 0; i < ScenesLoading.Count; i++)
        {
            while (!ScenesLoading[i].isDone)
            {
                progress += ScenesLoading[i].progress;
                ProgressBar.fillAmount = progress / ScenesLoading.Count;
                yield return null;
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync("Menu Scene");
    }
}
