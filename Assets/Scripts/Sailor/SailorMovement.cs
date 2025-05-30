using System;
using CustomAttributes;
using UnityEngine;
using UnityEngine.AI;

namespace Sailor
{
    public class SailorMovement : MonoBehaviour
    {
        private new Transform transform;
        
        [Header("Values")]
        [ReadOnly] public float speed = 5f;
        public float stoppingDistance = 0.1f;

        [ReadOnly] public bool isAtDestination = true;
        
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

        /// <summary>
        /// Move toward the current path corner
        /// </summary>
        private void Move()
        {
            Vector3 _destination = currentPath.corners[currentCornerIndex];
            Vector3 _modifiedYDestination = new Vector3(_destination.x, transform.position.y, _destination.z); //destination ignoring Y
            
            transform.position = Vector3.MoveTowards(transform.position, _modifiedYDestination, Time.deltaTime * speed);
            
            if (Vector3.Distance(transform.position , _modifiedYDestination) < stoppingDistance) //check if at path corner 
            {
                currentCornerIndex++;
                if (currentCornerIndex >= currentPath.corners.Length) //check if at destination
                {
                    currentPath = null;
                    currentCornerIndex = 0;
                    isAtDestination = true;
                }
            }

            SetYPositionOnGround();
        }
        
        public void SetSpeed(float _newSpeed)
        {
            speed = _newSpeed;
        }

        public void SetDestination(Vector3 _destination)
        {
            var _newPath = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, _destination, NavMesh.AllAreas, _newPath))
            {
                currentPath = _newPath;
                currentCornerIndex = 0;
                isAtDestination = false;
            }
            else
            {
                Debug.LogWarning("Path not valid");
            }
        }

        /// <summary>
        /// Set the destination to a random point on the navmesh
        /// </summary>
        public void SetDestinationToRandomPoint()
        {
            if (!NavMesh.SamplePosition(UnityEngine.Random.insideUnitSphere * 5, 
                    out var _hit, 5, NavMesh.AllAreas)) //get a random point on the navmesh
            {
                return;
            }
            Vector3 _randomNavMeshPoint = _hit.position;
            SetDestination(_randomNavMeshPoint);
        }
        
        /// <summary>
        /// Set the sailor on the ground
        /// </summary>
        private void SetYPositionOnGround()
        {
            float _rayLength = 10f;
            if (Physics.Raycast(transform.position, Vector3.down, out var _hit, _rayLength, ~(1 << LayerMask.NameToLayer("Task"))))
            {
                Vector3 _pos = transform.position;
                _pos.y = _hit.point.y + 2f * 0.5f;
                transform.position = _pos;
            }
        }
    }
}