using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Waypoint :: Utilizado para atualização dos vértices, ou seja, ao mover ou deletar um vértice.
 */
[ExecuteInEditMode]
public class WayPoint : MonoBehaviour
{
    
    private void OnEnable()
    {
        int index = 0;
        if (int.TryParse(name, out index) == false)
        {
            return;
        }
        transform.parent.parent.GetComponent<WayPointGrid>().UpdateVertexAvailability(index, true);
    }

    private void OnDisable()
    {
        int index = 0;
        if (int.TryParse(name, out index) == false)
        {
            return;
        }
        transform.parent.parent.GetComponent<WayPointGrid>().UpdateVertexAvailability(index, false);
    }

    void Update()
    {
        int index = 0;
        if (int.TryParse(name, out index) == false)
        {
            return;
        }
        transform.parent.parent.GetComponent<WayPointGrid>().UpdateVertexPosition(index, transform.position);
    }
}
