//practice
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToOrigin : MonoBehaviour
{
    private Vector3 _originalPosition;
    private bool _isDragging;
    public float returnTime = 3.0f;

    void Start()
    {
        _originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject == gameObject)
            {
                _isDragging = true;
            }
        }

        if (_isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
            transform.position = newPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            StartCoroutine(ReturnToOriginalPosition());
        }
    }

    IEnumerator ReturnToOriginalPosition()
    {
        yield return new WaitForSeconds(returnTime);

        while (Vector3.Distance(transform.position, _originalPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _originalPosition, Time.deltaTime);
            yield return null;
        }

        transform.position = _originalPosition;
    }
}