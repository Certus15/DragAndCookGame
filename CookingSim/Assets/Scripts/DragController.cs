using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public DraggingItems LastDragged => lastDragged;

    private bool isDragActive = false;
    private Vector2 screenPosition;
    private Vector2 worldPosition;
    private DraggingItems lastDragged;

    public AudioSource myFX;
    public AudioClip pickFX;
    public AudioClip dropFX;

    private void Awake()
    {
        DragController[] controller = FindObjectsOfType<DragController>();
        if (controller.Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(isDragActive)
        {
            if(Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                Drop();
                return;
            }
        }
       

        if(Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount > 0)
        {
            screenPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (isDragActive)
        {
            Drag();
        }
        else
        {
            
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if(hit.collider != null)
            {
                DraggingItems draggable = hit.transform.gameObject.GetComponent<DraggingItems>();
                if(draggable != null)
                {
                    lastDragged = draggable;
                    InitDrag();
                }
            }
        }
    }

    private void InitDrag()
    {
        UpdateDragStatus(true);
        myFX.PlayOneShot(pickFX);
    }

    private void Drag()
    {
        lastDragged.transform.position = Vector2.MoveTowards(lastDragged.transform.position, new Vector2(worldPosition.x, worldPosition.y), 3);

    }

    private void Drop()
    {
        UpdateDragStatus(false);
        myFX.PlayOneShot(dropFX);
    }

    private void UpdateDragStatus(bool isDragging)
    {
        isDragActive = lastDragged.isDragging = isDragging;
        
        if (isDragging)
        {
            lastDragged.gameObject.layer = Layer.Dragging;
        }
        else
        {
            lastDragged.gameObject.layer = Layer.Default;
        }
    }
}