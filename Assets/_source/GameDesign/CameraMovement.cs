﻿using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.InputServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameDesign
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 50f;
        [SerializeField] private float _scrollPower = 1;

        private IInputService InputService => ServiceLocator.Instance.Get<IInputService>();

        void Update()
        {
            MoveXZ();
            MoveY();
        }

        private void MoveY()
        {
            Vector2 cameraMovement = InputService.CameraMovementY;
            Vector3 newPosition = transform.position - new Vector3(0, cameraMovement.y * _scrollPower * Time.deltaTime, 0);
            transform.position = newPosition;
        }

        private void MoveXZ()
        {
            Vector2 cameraMovement = InputService.CameraMovementXZ;

            if (cameraMovement == Vector2.zero)
                return;

            float cameraMovementX = cameraMovement.x * _movementSpeed * Time.deltaTime;
            float cameraMovementY = cameraMovement.y * _movementSpeed * Time.deltaTime;
            Vector3 position = transform.position;
            Vector3 newPosition = position + new Vector3(cameraMovementX, 0, cameraMovementY);

            float maxDistanceDelta = _movementSpeed * Time.deltaTime;
            position = Vector3.MoveTowards(position, newPosition, maxDistanceDelta);
            transform.position = position;
        }
    }
}