using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelView : MonoBehaviour
{
    public Level level;
    public LevelsView levelsView;
    public LevelManager levelManager;

    private void Awake(){
        levelManager = GameObject.FindWithTag("GameManager").GetComponent<LevelManager>();
    }
    public void OnPointerEnter(){
        levelsView.UpdateText(level.levelTitle);
    }

    public void OnPointerExit(){
        levelsView.UpdateText("");
    }

    public void OnClick(){
        StartLevel();
    }

    public void StartLevel(){
        Debug.Log($"Iniciando el nivel: {level.levelTitle}");
        levelManager.GoToLevel(level);
    }
}
