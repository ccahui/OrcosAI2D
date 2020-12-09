using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
ES IMPORTANTE QUE LA VISTA DE LA CAMARA SEA ORTOGONAL, PARA QUE LA SELECCION SEA PRECISA

ES IMPORTANTE QUE EL TERRENO O PLANO ESTE EN EL LAYER 8

ESTE SCRIPT PUEDE SER VINCULADO A CUALQUIER GAMEOBJECT QUE SIEMPRE ESTE ACTIVO, COMO LA CAMARA
*/
public class SelectionSquare : MonoBehaviour
{
   public List<GameObject> selectedUnits = new List<GameObject>(); //contendra a la(s) unidad(es) seleccionada(s)
    //rectangulo de seleccion
    public RectTransform selectionSquareTrans; //es una imagen dentro de un canvas, (la imagen puede tener un color de fondo rojo)
    //meteriales
    public Material selectedMaterial; //material que se aplicara a las unidades seleccionadas (puede ser un material de color)
    public Material normalMaterial; //material que se le aplica a las unidades no seleccioonadas (puede ser un material de color)

    private float limiteMovimiento = 0.5f; // distancia del punto donde se pulsa el mouse y el de donde se levanta
    private string tagObjetivo = "Aliado"; // tag que tendran las unidades seleccionables

    Vector3 squareStartPos; //alamacena el punto inicial cuando el mouse se presiona
    Vector3 squareMiddlePos; //alamance el punto del mouse cuando se mueve
    Vector3 squareEndPos; //almacena el punto fianl cuando el mouse se levanta

    

    // Start is called before the first frame update
    void Start()
    {
        selectionSquareTrans.gameObject.SetActive(false);
        DeseleccionarUnidades();
    }

    // Update is called once per frame
    void Update()
    {
        ControlarSeleccion();
    }

    void ControlarSeleccion()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            squareStartPos = Camera.main.ScreenToWorldPoint(mousePos);
            selectionSquareTrans.gameObject.SetActive(true);        

        }
        
        if (Input.GetMouseButton(0))
        {
            squareMiddlePos = Input.mousePosition;
            PreviewSquare();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            selectionSquareTrans.gameObject.SetActive(false);


            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            squareEndPos = Camera.main.ScreenToWorldPoint(mousePos);


            if (Vector3.Distance (squareStartPos, squareEndPos) <= limiteMovimiento){//estoy tratando de hacer click en una unidad
                SeleccionarUnidad();

            }
            else// estoy tratando de seleccionar varias unidades
            {

                 DeseleccionarUnidades();
                 SeleccionarUnidades();
            }
            ActualizarAspectoUnidadesSeleccionadas();
        }            
    }

    void PreviewSquare()
    {
        Vector3 squareStartScreen = Camera.main.WorldToScreenPoint(squareStartPos);
        squareStartScreen.z = 0f;
        Vector3 middle = (squareStartScreen + squareMiddlePos) / 2f;
        selectionSquareTrans.position = middle;

        float sizeX = Mathf.Abs(squareStartScreen.x - squareMiddlePos.x);
        float sizeY = Mathf.Abs(squareStartScreen.y - squareMiddlePos.y);

        selectionSquareTrans.sizeDelta = new Vector2(sizeX, sizeY);
    }

    void DeseleccionarUnidades()
    {
        for (int i = 0; i < selectedUnits.Count; i++)
        {
           // selectedUnits[i].GetComponent<MeshRenderer>().material = normalMaterial;
        }
        selectedUnits.Clear();
    }

    void SeleccionarUnidad()
    {

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0) ;


        if (hit.collider && hit.collider.CompareTag(tagObjetivo))
            {
            DeseleccionarUnidades();
            selectedUnits.Add(hit.collider.gameObject);
            Debug.Log("Seleccionando un " + tagObjetivo);

        } else
            {/*Estoy apuntando a un punto dentro del suelo ir al destino*/
                Vector2 destino = new Vector2(worldPosition.x, worldPosition.y);
            if (selectedUnits.Count > 0)
                {
                    foreach (GameObject obj in selectedUnits)
                    {
                        Debug.Log("Desplazando "+selectedUnits.Count+ " Unidades "+destino);
                        //obj.GetComponent<UnitPath>().desplazarme(destino);
                    }
                }
                DeseleccionarUnidades();
            }
        
    }

    void SeleccionarUnidades()
    {        
        GameObject[] allUnits = GameObject.FindGameObjectsWithTag(tagObjetivo);

        Vector2 punto1 = new Vector2(Mathf.Min(squareStartPos.x, squareEndPos.x),
                Mathf.Min(squareStartPos.y, squareEndPos.y));
        Vector2 punto2 = new Vector2(Mathf.Max(squareStartPos.x, squareEndPos.x),
                Mathf.Max(squareStartPos.y, squareEndPos.y));

        //Select the units
        for (int i = 0; i < allUnits.Length; i++)
        {
            GameObject currentUnit = allUnits[i];
            if(ContieneRectangulo(punto1, punto2, currentUnit.transform.position))
            {
                selectedUnits.Add(currentUnit);
            }
        }
        Debug.Log("Seleccionando Varios " + tagObjetivo + " " + selectedUnits.Count);
    }

    bool ContieneRectangulo(Vector2 punto1, Vector2 punto2, Vector3 position)
    {
        return punto1.x <=  position.x && position.x <= punto2.x && punto1.y <= position.y && position.y <= punto2.y;
    }

    /*
    Este metodo se ejecuta cuando se selecciona a las unidades, usala para alterar el aspecto o la animacion, etc.
    */
    void ActualizarAspectoUnidadesSeleccionadas()
    {
        for (int i = 0; i < selectedUnits.Count; i++)
        {
         //   selectedUnits[i].GetComponent<MeshRenderer>().material = selectedMaterial;
        }
    }
}
