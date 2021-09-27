using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiSceneLoader : MonoBehaviour
{
    [SerializeField] private string[] LoadScenes;
    [SerializeField] private string[] UnLoadScenes;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LoadScenes.Length > 0)
        {
            for (int i = 0; i < LoadScenes.Length; i++)
            {
                SceneManager.LoadSceneAsync(LoadScenes[i], LoadSceneMode.Additive);
            }
        }

        if (UnLoadScenes.Length > 0)
        {
            for (int i = 0; i < UnLoadScenes.Length; i++)
            {
                SceneManager.UnloadSceneAsync(UnLoadScenes[i]);
                
            }
        }
    }
}
