using System;
using UnityEngine;
using UnityEngine.AI;

namespace Sailor
{
    public class SailorMovement : MonoBehaviour
    {
        private new Transform transform;
        
        [Header("Values")]
        public float speed = 5f;
        public float rotationSpeed = 720f;
        public float stoppingDistance = 0.1f;
        
        private NavMeshPath currentPath;
        private int currentCornerIndex;

        private void Awake()
        {
            transform = GetComponent<Transform>();
        }

        private void Update()
        {
            if (currentPath == null)
            {
                return;
            }

            Move();
        }

        private void Move()
        {
            Vector2 _flatPosition = new Vector2(transform.position.x, transform.position.z);
            Vector2 _flatDestination = new Vector2(currentPath.corners[currentCornerIndex].x, currentPath.corners[currentCornerIndex].z);
            Vector3 _modifiedYDestination = new Vector3(_flatDestination.x, transform.position.y, _flatDestination.y);
            
            transform.position = Vector3.MoveTowards(transform.position, _modifiedYDestination, Time.deltaTime * 5f);
            
            if (Vector3.Distance(transform.position , _modifiedYDestination) < stoppingDistance)
            {
                currentCornerIndex++;
                if (currentCornerIndex >= currentPath.corners.Length)
                {
                    currentPath = null;
                    currentCornerIndex = 0;
                }
            }

            SetYPositionOnGround();
        }

        public void SetDestination(Vector3 _destination)
        {
            var _newPath = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, _destination, NavMesh.AllAreas, _newPath))
            {
                currentPath = _newPath;
                currentCornerIndex = 0;
            }
            else
            {
                Debug.LogWarning("Path not valid");
            }
        }

        private void SetYPositionOnGround()
        {
            float _rayLength = 10f;
            if (Physics.Raycast(transform.position, Vector3.down, out var _hit, _rayLength))
            {
                Vector3 _pos = transform.position;
                _pos.y = _hit.point.y + 2f * 0.5f;
                transform.position = _pos;
            }
        }
    }
}