using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float VIDA_FINAL = 600;
    public int vida;

    HealthBar healthBar;
    void Awake()
    {
        GameObject gameObject = transform.Find("HealthBar").gameObject;
        healthBar= gameObject.GetComponent<HealthBar>();
       
    }
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetSize(1);
        vida = (int)VIDA_FINAL;
    }
    public void enemeyAttack(int daño)
    {
     
        vida = vida - daño;
        if(vida <= 0)
        {
            healthBar.SetSize(0);
            vida = 0;
        }
        else
        {
            float size = (vida * 1f) / VIDA_FINAL;
            healthBar.SetSize(size);
        }
    }
    public int GetVida()
    {
        return vida;
    }
}
