using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Stone.control
{
    public class controlWoods : MonoBehaviour
    {
        public Vector3 startPos;
        public Vector3 endPos;
        public float speed = 5;
        public bool startMove;
        private void Start()
        {
            
        }
        private void Update()
        {
            if (startMove)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPos, speed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 20 * Time.deltaTime);
                
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Collider>().isTrigger = true;
            }

            StopMove();
        }

        public void StopMove()
        {
            if (transform.localPosition == endPos)
            {
                transform.rotation = Quaternion.identity;
                if(startMove)
                    startMove = false;
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<Collider>().isTrigger = true;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Sell"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
