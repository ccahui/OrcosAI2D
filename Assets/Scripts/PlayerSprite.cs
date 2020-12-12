using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    PlayerBehavior playerBehavior;
    // Start is called before the first frame update
    void Awake()
    {
        playerBehavior = gameObject.GetComponentInParent<PlayerBehavior>();

    }
    public void TriggerCooling()
    {
        Debug.Log("Golpe");
       // playerBehavior.TriggerCooling();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
