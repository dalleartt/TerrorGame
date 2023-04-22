using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaccion : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode interactionKey = KeyCode.E;
    public Vector3 rotationAmount;
    public Vector3 positionOffset;
    public float interactionTime = 1.0f;

    private bool _isInteracting = false;
    private float _interactionProgress = 0.0f;
    private Vector3 _initialRotation;
    private Vector3 _initialPosition;

    void Start()
    {
        _initialRotation = transform.eulerAngles;
        _initialPosition = transform.position;
    }

    void Update()
    {
        if (_isInteracting)
        {
            _interactionProgress += Time.deltaTime / interactionTime;

            // Lerp rotation and position
            transform.eulerAngles = Vector3.Lerp(_initialRotation, _initialRotation + rotationAmount, _interactionProgress);
            transform.position = Vector3.Lerp(_initialPosition, _initialPosition + positionOffset, _interactionProgress);

            if (_interactionProgress >= 1.0f)
            {
                _isInteracting = false;
                _initialRotation = transform.eulerAngles;
                _initialPosition = transform.position;
                positionOffset = positionOffset * -1;
                rotationAmount = rotationAmount * -1;
            }
        }
    }

    public void Interact()
    {
        if (!_isInteracting)
        {
            _isInteracting = true;
            _interactionProgress = 0.0f;
        }
    }
}
