using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    LevelsView levelsView;
    Level currentLevel;
    private EscMenuView escMenu;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        escMenu = GameObject.FindWithTag("EscMenu")
            .GetComponent<EscMenuView>();
    }

    // Start is called before the first frame update
    public void GoToLevel(Level level)
    {
        currentLevel = level;
        SceneManager.LoadScene(level.sceneName);
    }

    public void GoToNextLevel()
    {
        GoToLevel(currentLevel.nextLevel);
    }

    public void ShowFinishMessage()
    {
        EscMenuView.instance.Show();
        escMenu.VictoryMessage = currentLevel.levelTitle;
    }
}
