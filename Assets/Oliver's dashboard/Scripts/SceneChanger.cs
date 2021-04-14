using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }


        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public static void ChangeScene(string sceneName, bool showLoadingScreen = true)
    {
        Instance.StartCoroutine(Instance.LoadSceneAsync(sceneName, showLoadingScreen));
    }

    private IEnumerator LoadSceneAsync(string sceneName, bool showLoadingScreen = true)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Cannot load scene using null or empty string!");
            yield break;
        }
        DOTween.KillAll(true);

        if(showLoadingScreen) 
            CanvasLoading.Instance.Show();

        yield return new WaitForSeconds(0.5f);

        yield return SceneManager.LoadSceneAsync(sceneName);
        
        
        if(showLoadingScreen)
            CanvasLoading.Instance.Hide();
    }
}
