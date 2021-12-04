using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;
using static AssScript;

public class Card : MonoBehaviour 
     , IPointerClickHandler
     , IDragHandler
     , IPointerEnterHandler
     , IPointerExitHandler
{
    // The canvas to render everything on
    public Canvas canvas;
    // Card Text
    public Text text;
    // Get rotation and position the card spawned as
    Quaternion originRotation;
    Vector3 originPosition;

    // Booleans for dragging, hovering, and if the card is moveable
    bool bisDragging = false;
    bool bisHovered = false;
    bool bCanMove = false;

    void Awake() 
    {
        // Set Raycasts to detect colliders
        Physics.queriesHitTriggers = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set original position
        originPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // If card is being dragged or not
        if (bisDragging && bisHovered)
        {
            transform.position = Input.mousePosition;
            //bisHovered = false;
        }
        else
        {
            // If not hovering over the card, then set to origin
            // ONLY if the card was dragged
            if(!bisHovered && bCanMove) transform.position = originPosition;
        }

        // If player stopped dragging, then set dragging to false
        if (Input.GetMouseButtonUp(0))
        {
            bisDragging = false;
            bisHovered = false;
        }
    }

    // On Pointer Clicked
    public void OnPointerClick(PointerEventData eventData) // 3
    {
        Debug.Log(gameObject.name + " Was Clicked!");
    }

    // On Pointer Dragged
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + " Is being Dragged!");
        bisDragging = true;
    }

    // On Pointer Enter
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + " Pointer has entered!");

        // Save card position
        originPosition = transform.position;

        // Allow card to be moved, and if so, when it's released move it back
        bCanMove = true;

        // Say the card's hovered
        bisHovered = true;


        // Increase card's size
        transform.localScale = new Vector3(3, 3, 3);

        // Move card up in position
        transform.position = transform.position + new Vector3(0, 70, 1);

        // Set the card rotation to be readable
        originRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        // Get Set Sort Order to be in front
        canvas.sortingOrder = 10;
    }

    // On Pointer Exit
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + " Pointer just exited!");

        // Say the card's not hovered
        bisHovered = false;

        // Set card's size back to normal
        transform.localScale = new Vector3(2, 2, 2);

        // Set the card rotation back to what it was
        transform.rotation = originRotation;

        // Set the card position back to normal
        transform.position = originPosition;

        // Stop the card from snapping back
        bCanMove = false;

        // Get Set Sort Order to be in front
        canvas.sortingOrder = 0;
    }
}
