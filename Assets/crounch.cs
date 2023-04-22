using JetBrains.Annotations;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class crounch : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode crouchKey = KeyCode.C;
    private FirstPersonController controller;
    private float speedCrounch;
    private CharacterController characterController;
    private bool _isCrouching = false;

    private float crouchHeight;
    public float standingHeight = 2.0f;


    //logica para pararse si entra en el sitio
    public LayerMask obstacleMask;

    void Start()
    {
        controller= GetComponent<FirstPersonController>();
        characterController = GetComponent<CharacterController>();
        speedCrounch = 2;
        crouchHeight = characterController.height / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(crouchKey))
        {

            if (_isCrouching && CanStandUp())
            {                
                if (_isCrouching)
                {
                    controller.MoveSpeed = 4;
                    controller.SprintSpeed = 6;
                }
                else
                {
                    controller.MoveSpeed = speedCrounch;
                    controller.SprintSpeed = 3;
                }
                _isCrouching = !_isCrouching;
            }
            else if (!_isCrouching)
            {
                if (_isCrouching)
                {
                    controller.MoveSpeed = 4;
                    controller.SprintSpeed = 6;
                }
                else
                {
                    controller.MoveSpeed = speedCrounch;
                    controller.SprintSpeed = 3;
                }
                _isCrouching = !_isCrouching;
            }

            
        }

        float targetHeight = _isCrouching ? crouchHeight : standingHeight;
        float currentHeight = Mathf.Lerp(characterController.height, targetHeight, speedCrounch * Time.deltaTime);
        characterController.height = currentHeight;

        // Ajusta la posición en Y del personaje para evitar que se hunda en el suelo al agacharse
        Vector3 currentPosition = transform.position;
        currentPosition.y += (currentHeight - characterController.height) / 2;
        transform.position = currentPosition;

        





    }
    public bool CanStandUp()
    {



        // Calcula la posición inicial y la dirección del raycast
        Vector3 rayOrigin = transform.position + Vector3.up * (crouchHeight / 2);
        Vector3 rayDirection = Vector3.up;

        // Distancia que el rayo debe recorrer para verificar si hay espacio suficiente para levantarse

        // Verifica si hay espacio suficiente sobre el personaje para levantarse
        //EL 2F deberia hacerlo dinamico, ya que depende de la altura del personaje.
        bool isSpaceAbove = !Physics.Raycast(rayOrigin, rayDirection, 2f, obstacleMask);
        Debug.Log(rayOrigin);
        Debug.Log(rayDirection);

        Debug.Log(isSpaceAbove);
        return isSpaceAbove;
    }
}
