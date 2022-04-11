using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Wood.Control
{
    public class controlWoods : MonoBehaviour
    {
        public Vector3 endPos;
        public float moveSpeed = 5;

        public float WoodCost = 5;
        public bool startMove;
        public bool isDestroy;
        private void Start()
        {
            
        }
        private void Update()
        {
            moveObject();
            if (isDestroy)
                Destroy(this.gameObject, 0.1f);

        }



        public void moveObject()
        {
            if (startMove)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPos, moveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 20 * Time.deltaTime);

                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Collider>().isTrigger = true;
            }
            if (transform.localPosition == endPos)
            {
                transform.localPosition = endPos;
                transform.rotation = Quaternion.identity;
                if (startMove)
                {
                    GetComponent<Collider>().isTrigger = false;
                    startMove = false;
                }
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
                FindObjectOfType<Core.GameManager>().maxCash += WoodCost;
                Destroy(this.gameObject);
            }
        }
    }
}
