using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EscMenuView : MonoBehaviour
{
    public static EscMenuView instance;

    [SerializeField]
    private TextMeshProUGUI victoryMessage;
    [SerializeField]
    public GameObject escMenu;
    static public bool isShown;

    private void Awake(){
        if(instance == null){
            instance = this;
        }
    }

    private void Start(){
        Hide();
    }
    public string VictoryMessage {
        get => victoryMessage.text;
        set {
            victoryMessage.text = value;
        }
    }
    // Start is called before the first frame update
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isShown){
                Hide(); 
            } else {
                Show();
            }
        }
    }

    public void Show(){
        isShown = true;
        escMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Hide(){
        isShown = false;
        escMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
