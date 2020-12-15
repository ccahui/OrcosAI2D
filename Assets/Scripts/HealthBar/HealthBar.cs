using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;

    Quaternion rotation;

    void LateUpdate()
    {
       // transform.rotation = rotation;
    }
    private void Awake()
    {
        bar = transform.Find("Bar");
        float health = Random.Range(0.1f, 1f);
        SetSize(health);
        rotation = transform.rotation;
    }
    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1, 1);
    }

}
