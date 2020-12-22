using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuView : MonoBehaviour
{
    [SerializeField]
    private GameObject levelsView;
    public void Awake(){
        levelsView = GameObject.FindWithTag("LevelSelectorMenu");
    }
    // Start is called before the first frame update
    public void GoToLevelSelector(){
        levelsView.SetActive(true);
        gameObject.SetActive(false);
    }
}
