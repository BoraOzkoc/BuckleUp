using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private float targetSpeed;
    private float _currentSpeed;
    private NavMeshAgent _agent;

    private void Awake()
    {
        SetAgent();
    }

    private void SetAgent()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = targetSpeed;
    }

    void Update()
    {
        Move();
    }
    private void Move()
    {
        float y_Axis = variableJoystick.Vertical;
        float x_Axis = variableJoystick.Horizontal;
        

        Vector3 direction = Vector3.forward * y_Axis + Vector3.right * x_Axis;
        
        LookTowardsDirection(direction);

        Vector3 destination = transform.position + direction * targetSpeed;
        
        _agent.SetDestination(destination);

    }
    

    private void LookTowardsDirection(Vector3 dir)
    {
        transform.LookAt(transform.position + dir);
    }

    private void IncreaseSpeed()
    {
        if (variableJoystick.Vertical == 0 && variableJoystick.Horizontal == 0) ResetSpeed();
        else
        {
            if (_currentSpeed < targetSpeed) _currentSpeed += 0.07f;

        }
    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        if (Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_agent.destination,1);
        }
    }

    private void ResetSpeed()
    {
        _currentSpeed = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetSpeed();
    }

    public void OnPointerDown()
    {
        
    }
}
