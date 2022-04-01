using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stone.Control
{
    public class controlCutter : MonoBehaviour
    {

        public Vector3 RotationSpeed;        
        private void Update()
        {
            transform.Rotate(RotationSpeed * Time.deltaTime, Space.Self);
        }   
    }
}
    
