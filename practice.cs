//practice
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// take name space which is needed

public class ReturnToOrigin : MonoBehaviour //define MonoBehaviour class named ReturnToOrigin 
{
    private Vector3 _originalPosition; // save origin position
    private bool _isDragging; // is dragging?
    public float returnTime = 3.0f; // time to return

    void Start()
    {
        _originalPosition = transform.position; //save origin position
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //make ray as mouse pointer position when input left button of mouse

            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject == gameObject) //object is dragging
            {
                _isDragging = true;
            }
        }

        if (_isDragging) //update object position follow mouse pointer position
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
            transform.position = newPosition;
        }

        if (Input.GetMouseButtonUp(0)) //if left button drag off
        {
            _isDragging = false;
            StartCoroutine(ReturnToOriginalPosition());
        }
    }

    IEnumerator ReturnToOriginalPosition()
    {
        yield return new WaitForSeconds(returnTime); //wait for returnTime

        while (Vector3.Distance(transform.position, _originalPosition) > 0.01f) //object position close to origin position
        {
            transform.position = Vector3.MoveTowards(transform.position, _originalPosition, Time.deltaTime); //control move
            yield return null;
        }

        transform.position = _originalPosition; // setting origin position correctly
    }
}

// is mouse dragging and off, after return time, object start to return origin position