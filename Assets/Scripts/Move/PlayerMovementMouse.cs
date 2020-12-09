using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMouse : MonoBehaviour
{
    private IMovePosition iMovePosition;

    private void Awake()
    {
        iMovePosition = GetComponent<IMovePosition>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 posicion = positionMouse();
            iMovePosition.SetMovePosition(posicion);
        }
    }

    private Vector3 positionMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        return worldPosition;
    }
}
