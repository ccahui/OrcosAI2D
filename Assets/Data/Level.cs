using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Orcs/Level", order = 1)]
public class Level : ScriptableObject{
    public string sceneName;
    public string levelTitle;
    public bool isActive;
    public Level nextLevel;
    public int numberOfEnemies;
}