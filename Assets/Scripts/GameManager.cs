
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager

    public static GameManager Instance;
    public int recurso = 0;

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


}
