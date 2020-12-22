using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelsView : MonoBehaviour
{
    public LevelView levelViewPrefab;
    public List<Level> levels;
    public TextMeshProUGUI levelTitleText;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Level level in levels){
            LevelView levelView = GameObject.Instantiate(
                levelViewPrefab, transform);

            levelView.level = level;
            levelView.levelsView = this;
        }
    }

    public void UpdateText(string text){
        levelTitleText.text = text;
    }
}
