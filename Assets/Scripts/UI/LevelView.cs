using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelView : MonoBehaviour
{
    public Level level;
    public LevelsView levelsView;
    public LevelManager levelManager;
    public Image image;

    private void Awake(){
        levelManager = GameObject.FindWithTag("GameManager").GetComponent<LevelManager>();
        image = GetComponent<Image>();
    }

    private void Start(){
        image.sprite = level.imageSprite;
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
