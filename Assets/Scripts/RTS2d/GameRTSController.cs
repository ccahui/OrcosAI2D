using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRTSController : MonoBehaviour
{
    [SerializeField] private Transform selectionArea;
    // Start is called before the first frame update
    private Vector3 startPosition;
    private List<UnitRTS> unitsList;
    private void Awake()
    {
        unitsList = new List<UnitRTS>();
        selectionArea.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = position();
            selectionArea.gameObject.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = position();
            Vector3 lower = new Vector3(Mathf.Min(startPosition.x, currentMousePosition.x), Mathf.Min(startPosition.y, currentMousePosition.y));
            Vector3 upper = new Vector3(Mathf.Max(startPosition.x, currentMousePosition.x), Mathf.Max(startPosition.y, currentMousePosition.y));

            selectionArea.position = lower;
            selectionArea.localScale = upper - lower;
        }
        if (Input.GetMouseButtonUp(0))
        {
            selectionArea.gameObject.SetActive(false);
            deseleccionarUnidades();
            unitsList.Clear();
            seleccionarUnidades();
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 targetPosition = position();

            // Una unidad
            if(unitsList.Count == 1)
            {
                unitsList[0].MoveTo(targetPosition);
                return;
            }
            //Varias Unidades 
            List<Vector3> targetPositionList = GetPositionListAround(targetPosition, new float[] { 10f, 20f, 30f}, new int[] {5,10,20});
            int index = 0;
            foreach(UnitRTS unit in unitsList)
            {
                unit.MoveTo(targetPositionList[index]);
                index = (index + 1) % targetPositionList.Count;
            }
        }

    }


    private Vector3 position()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition  = Camera.main.ScreenToWorldPoint(mousePos);

        return worldPosition;
    }

    private List<Vector3> GetPositionListAround(Vector3 startPosition, float[] ringDistance, int [] ringPositionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for(int i = 0; i < ringDistance.Length; i++)
        {
            positionList.AddRange(GetPositionListAround(startPosition, ringDistance[i], ringPositionCount[i]));
        }
        return positionList;
    }
    private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for(int i = 0; i < positionCount; i++)
        {
            float angle = i * (360f / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }
    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }
    private void seleccionarUnidades()
    {
        Collider2D[] colliders = Physics2D.OverlapAreaAll(startPosition, position());
        foreach (Collider2D collider2D in colliders)
        {
      //      Debug.Log(collider2D.name);
            UnitRTS unitRts = collider2D.GetComponent<UnitRTS>();
            if (unitRts != null)
            {
                unitRts.SetSeletedVisible(true);
                unitsList.Add(unitRts);
            }
        }
      //  Debug.Log(unitsList.Count);

    }
    private void deseleccionarUnidades()
    {
        foreach (UnitRTS unit in unitsList)
        {
            unit.SetSeletedVisible(false);
        }
    }
}
