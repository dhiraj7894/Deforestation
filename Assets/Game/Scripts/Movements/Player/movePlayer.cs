using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stone.movement
{
    public class movePlayer : MonoBehaviour
    {
        public Joystick Joystick;
        public float speed;
        public float rotationSmooth;

        private CharacterController CharacterController;
        private float turnSmoothVelocity;
        //private Transform cam;


        private Rigidbody playerBody;

        

        void Start()
        {
            playerBody = GetComponent<Rigidbody>();
            //CharacterController = GetComponent<CharacterController>();
            //cam = Camera.main.transform;
        }

        
        void Update()
        {
            
            //transform.localPosition = new Vector3(transform.localPosition.x, 1f, transform.localPosition.z);
        }
        private void FixedUpdate()
        {
            MovePlayer();
        }

        [HideInInspector]public Vector3 direction;
        public void MovePlayer()
        {
            float x = Joystick.Horizontal;
            float z = Joystick.Vertical;

            direction = new Vector3(x,0, z).normalized;

            if (direction.magnitude > 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmooth);
                transform.rotation = Quaternion.Euler(0, angle, 0);
                Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                Vector3 move = moveDir.normalized;                
                playerBody.velocity = new Vector3(move.x, 0, move.z) * speed;
                //CharacterController.Move(moveDir.normalized * speed * Time.deltaTime);
                
            }
        }

    }
}
