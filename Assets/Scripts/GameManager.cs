
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
        Debug.Log("Game Over");
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
        Debug.Log("Pasate de Nivel");
    }

}
