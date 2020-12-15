
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager

    public static GameManager Instance;
    public int recurso = 0;
    public List<EnemyBehavior> enemies;
    public List<PlayerBehavior> aliados;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Debug.Log("Iniciando ...");
    }

    #endregion

    public void aumentarRecurso()
    {
        recurso++;
        Debug.Log("Mato un enemigo +1 oro");
}
    public void gameOver()
    {
        foreach (PlayerBehavior aliado in aliados)
        {
            if(aliado.gameObject.activeSelf == true)
            {
                return;
            }
        }
        Debug.Log("GAME OVER: Dispone de +" + recurso + " oro");
    }
    public void pasasteDeNivel()
    {
        foreach (EnemyBehavior enemigo in enemies)
        {
            if (enemigo.gameObject.activeSelf == true)
            {
                return;
            }
        }
        Debug.Log("IR A SIGUIENTE NIVEL: Dispone de +" + recurso + " oro");
    }

}
