using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{

    private readonly static int levelSelectorSceneBuildIndex = 1;


    public void MoveIntoLevelSelector()
    {
        SceneManager.LoadScene(levelSelectorSceneBuildIndex);
    }


    public void LoadaLevel(int LevelNumber)
    {
        SceneManager.LoadScene(LevelNumber+1);
    }


    public void MoveToMain()
    {
        SceneManager.LoadScene(0);
    }



    public void Quit()
    {
        Application.Quit();
    }
}
