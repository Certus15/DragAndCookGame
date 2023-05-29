using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingItems : MonoBehaviour
{
    public string itemName;
    public bool isDragging;

    private Collider2D coll;
    private DragController dragController;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        dragController = FindObjectOfType<DragController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DraggingItems collidedDraggable = other.GetComponent<DraggingItems>();

        if(collidedDraggable != null && dragController.LastDragged.gameObject == gameObject)
        {
            ColliderDistance2D colliderDistance2D = other.Distance(coll);
            Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y) * colliderDistance2D.distance;
            transform.position -= diff;
        }
    }
}
