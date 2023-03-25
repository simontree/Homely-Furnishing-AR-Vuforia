using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{ 
    public void LoadTutorialScene()
    {
        SceneManager.LoadScene("1_TutorialScene", LoadSceneMode.Single);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("2_MainScene", LoadSceneMode.Single);
    }
}
