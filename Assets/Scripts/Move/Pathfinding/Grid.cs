using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;
    public bool displayGridGizmos;

    float nodeDiameter;
    int gridSizeX, gridSizeY;


    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();

    }
    public int MaxSize {
       get {
            return gridSizeX* gridSizeY;
       }
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                // bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                bool walkable = !(Physics2D.OverlapCircle(new Vector2(worldPoint.x, worldPoint.y), nodeRadius, unwalkableMask));
                //bool walkable = false;
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
        grid[11, 0].walkable = false;
    }

    public Node NodeFromWorldPoint(Vector3 _worldPosition)
    {
        float posX = ((_worldPosition.x - transform.position.x) + gridWorldSize.x * 0.5f) / nodeDiameter;
        float posY = ((_worldPosition.y - transform.position.y) + gridWorldSize.y * 0.5f) / nodeDiameter;

        posX = Mathf.Clamp(posX, 0, gridWorldSize.x - 1);
        posY = Mathf.Clamp(posY, 0, gridWorldSize.y - 1);

        int x = Mathf.FloorToInt(posX);
        int y = Mathf.FloorToInt(posY);

        return grid[x, y];
    }


    public List<Node> GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDrawGizmos() {

              if (grid != null && displayGridGizmos)
        {
            Gizmos.DrawCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, -5));

            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
              //  Gizmos.DrawSphere(n.worldPosition, nodeRadius);
                //Gizmos.DrawCube(n.worldPosition, n.worldPosition*3);

            }
        }

    }
    


}
