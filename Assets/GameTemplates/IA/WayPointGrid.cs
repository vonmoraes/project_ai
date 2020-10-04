using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class WayPointGrid : MonoBehaviour
{
    float DistanceBetweenVertices = 0.0f;
    int GridSize = -1;

    public float AttributeExample;

    public Vector3[,] VertexPositions = null; // SEMPRE A DISTÂNCIA ENTRE OS VERTEX - PESO
    public bool[,] VertexAvailability = null;
    public int[,] AdjacenceMatrix = null; // EU CRIEI

    public Transform WayPointPrefab;

    static WayPointGrid gInstance = null;


    // TEM EM OUTRO CODIGO DO FESSO
    public float[,] edgesWeight = null;
    public bool showEdgesGizmo = true;


    // TEM UM METODO CHAMADO UPDATEGRIDNAVICATION
    // TEM EM OUTRO CODIGO DO FESSO

    public static WayPointGrid GetInstance()
    {
        return gInstance;
    }

    void Awake()
    {
        gInstance = this;
    }

    public void LoadGrid()
    {
        // LOAD THE GRID
    }

    /**
     * Cria a matriz de posições dos vértices
     * */
    public void CreateWaypointGrid(int gridSize, float distanceBetweenVertices)
    {
        Transform gridEditorAux = transform.GetChild(0);

        DistanceBetweenVertices = distanceBetweenVertices;
        GridSize = gridSize;
        VertexPositions = new Vector3[GridSize, GridSize];
        VertexAvailability = new bool[GridSize, GridSize];
        AdjacenceMatrix = new int[GridSize, GridSize]; // EU
        // OUTRO COD
        edgesWeight = new float[GridSize, GridSize];



        //OUTRO COD

        // Calculate Start Position based an it's center
        float offset = gridSize * distanceBetweenVertices * 0.5f;
        Vector3 currentPosition = transform.position - new Vector3(offset, 0.0f, offset);

        // Create vertices
        for (int z = 0; z < GridSize; z++)
        {
            currentPosition.x = transform.position.x - offset;

            for (int x = 0; x < GridSize; x++)
            {
                VertexPositions[x, z] = currentPosition + transform.position;
                VertexAvailability[x, z] = true;
                //AdjacenceMatrix[x, z] = 0;

                // OUTRO COD
                if (x == z)
                {
                    edgesWeight[x, z] = 0f;
                }
                else
                {
                    edgesWeight[x, z] = DistanceBetweenVertices;
                }
                // OUTRO COD


                currentPosition.x += distanceBetweenVertices;

                int index = ((z * GridSize) + x);

                GameObject child = null;
                if (index >= gridEditorAux.childCount)
                {
                    child = Instantiate(WayPointPrefab, gridEditorAux).gameObject;
                    child.name = index.ToString();
                    child.transform.parent = gridEditorAux;
                }
                else
                {
                    child = gridEditorAux.GetChild(index).gameObject;
                }

                child.transform.position = currentPosition + transform.position;
            }

            currentPosition.z += distanceBetweenVertices;
        }
    }

   

    public void UpateWayPointVertex()
    {
        // UPDATE VERTEX COLLISION AND HEIGHT
    }

    // posição do vértice
    public void UpdateVertexPosition(int index, Vector3 position)
    {
        if (VertexPositions == null)
            return;

        int xIndex = index % GridSize;
        int zIndex = index / GridSize;

        VertexPositions[xIndex, zIndex] = position;
    }

    // se um vertice esta ativado ou não
    public void UpdateVertexAvailability(int index, bool enabled)
    {
        if (VertexAvailability == null)
            return;

        int xIndex = index % GridSize;
        int zIndex = index / GridSize;

        VertexAvailability[xIndex, zIndex] = enabled;
    }

    public int GetGridIndexByPosition(Vector3 position)
    {
        float offset = GridSize * DistanceBetweenVertices * 0.5f;
        float maxXPosition = offset * GridSize + transform.position.x;
        float maxZPosition = offset * GridSize + transform.position.z;

        int xIndex = (int) ((float) (position.x / maxXPosition) * GridSize);
        int zIndex = (int) ((float) (position.z / maxXPosition) * GridSize);

        return (zIndex * GridSize) + xIndex;
    }

    public float GetWeight(int fromIndex, int toIndex)
    {
        int fromXIndex = fromIndex % GridSize;
        int fromZIndex = fromIndex / GridSize;

        int toXIndex = toIndex % GridSize;
        int toZIndex = toIndex / GridSize;

        Vector3 fromPos = VertexPositions[fromXIndex, fromZIndex];
        Vector3 toPos = VertexPositions[toXIndex, toZIndex];

        return Vector3.Distance(fromPos, toPos);
    }

    public Vector3 GetVertexPosition(int xIndex, int zIndex)
    {
        return VertexPositions[xIndex, zIndex];
    }

    void OnDrawGizmos()
    {
        if (VertexPositions == null)
            return;

        // Draw Vertices
        Gizmos.color = Color.yellow;
        for (int x = 0; x < GridSize; x++)
        {
            for (int z = 0; z < GridSize; z++)
            {
                if (VertexAvailability[x, z])
                    Gizmos.DrawSphere(transform.position + VertexPositions[x, z], 0.25f);
            }
        }


        // Draw Edges
        Gizmos.color = Color.white;
        // HORIZONTAL
        for (int x = 0; x < GridSize; x++)
        {
            for (int z = 0; z < GridSize; z++)
            {
                try
                {
                    if (VertexAvailability[x, z] && VertexAvailability[x + 1, z])
                    {
                        Vector3 a = transform.position + VertexPositions[x, z];
                        Vector3 b = transform.position + VertexPositions[x + 1, z];
                        Gizmos.DrawLine(a, b);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    z = GridSize;
                }

            }
        }

        // VERTICAL
        for (int x = 0; x < GridSize; x++)
        {
            for (int z = 0; z < GridSize; z++)
            {
                try
                {
                    if (VertexAvailability[x, z] && VertexAvailability[x, z + 1])
                    {
                        Vector3 a = transform.position + VertexPositions[x, z];
                        Vector3 b = transform.position + VertexPositions[x, z + 1];
                        Gizmos.DrawLine(a, b);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    z = GridSize;
                }
            }
        }

        //// DIAGONAIS
        //for (int x = 0; x < GridSize; x++)
        //{
        //    for (int z = 0; z < GridSize; z++)
        //    {
        //        try
        //        {
        //            if (VertexAvailability[x, z])
        //            {
        //                // DIAGONAL DIREITA
        //                Vector3 a = transform.position + VertexPositions[x, z];
        //                Vector3 b = transform.position + VertexPositions[z, x];
        //                Gizmos.DrawLine(a, b);
        //                // DIAGONAL ESQUERDA
        //                Vector3 c = transform.position + VertexPositions[x, z];
        //                Vector3 d = transform.position + VertexPositions[x + 1, z + 1];
        //                Gizmos.DrawLine(c, d);
        //            }
        //        }
        //        catch (IndexOutOfRangeException)
        //        {
        //            z = GridSize;
        //        }
        //    }
        //}

    }


    public void printGrid()
    {
        string matrix = "(";
        for (int i = 0; i < GridSize; i++)
        {
            matrix += "[";
            for (int j = 0; j < GridSize; j++)
            {
                matrix += GetVertexPosition(i, j);//VertexAvailability[i, j] + " ";
            }
            matrix += "]";
        }
        matrix += ")";
        Debug.Log(matrix);
    }

    internal void Teste()
    {
        Vector3 position = transform.position - new Vector3(4, 0.0f, 8);
        UpdateVertexPosition(0,position);
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(position, 0.25f);
    }
}