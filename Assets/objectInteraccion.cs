using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectInteraccion : MonoBehaviour
{
    public string interactableTag;
    public float interactionDistance = 5.0f;
    public GUIStyle guiStyle;

    private Camera _camera;
    private GameObject _targetedObject;

    public KeyCode interactionKey = KeyCode.E;


    void Start()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.green, 1f);
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {

            if (hit.collider.CompareTag(interactableTag))
            {
                _targetedObject = hit.collider.gameObject;
                // Verifica si el jugador presiona la tecla de interacción
                if (Input.GetKeyDown(interactionKey))
                {
                    // Llama a la función Interact() del objeto interactuable
                    _targetedObject.GetComponent<Interaccion>().Interact();
                }


            }
            else
            {
                _targetedObject = null;
            }
        }
        else
        {
            _targetedObject = null;
        }
    }

    void OnGUI()
    {
        if (_targetedObject != null)
        {
            string objectName = _targetedObject.name;
            Vector2 textSize = guiStyle.CalcSize(new GUIContent(objectName));
            float textWidth = textSize.x;
            float textHeight = textSize.y;

            float screenX = Screen.width / 2 - textWidth / 2;
            float screenY = Screen.height / 2 - textHeight / 2;

            GUI.Label(new Rect(screenX, screenY - 50, textWidth, textHeight), objectName, guiStyle);
        }
    }
}
