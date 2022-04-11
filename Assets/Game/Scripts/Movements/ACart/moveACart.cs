using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wood.Move
{
    public class moveACart : MonoBehaviour
    {
        public Control.controlACartPathPoints controlACartPathPoints;
        public Control.controlACartInventory controlACartInventory;
        public float moveSpeed;
        public float rotationSpeed;
        public bool isMove;
        public bool isDestroy;

        public Transform inventory;

        int nextPositionIndex;
        Transform NextPos;
        Quaternion rotTarget;
        void Start()
        {            
            NextPos = controlACartPathPoints.pathPoint[0];
        }

        // Update is called once per frame
        void Update()
        {
            if (isMove)
                MoveACart();

            ClearCart();
        }

        void MoveACart()
        {
            if(transform.position == NextPos.position)
            {
                nextPositionIndex++;

                if (nextPositionIndex>= controlACartPathPoints.pathPoint.Length)
                {
                    //Destroy
                    nextPositionIndex = controlACartPathPoints.pathPoint.Length - 1;
                    isDestroy = true;
                }
                NextPos = controlACartPathPoints.pathPoint[nextPositionIndex];
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, NextPos.position, moveSpeed * Time.deltaTime);
                try
                {
                    rotTarget = Quaternion.LookRotation(new Vector3(NextPos.position.x, this.transform.position.y, NextPos.position.z) - this.transform.position);
                    this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTarget, rotationSpeed * Time.deltaTime);
                }
                catch
                {

                }
            }
        }
        float x = 0;
        void ClearCart()
        {
            if (isDestroy)
            {
                if (x > 0)
                    x -= Time.deltaTime;
                if (x <= 0)
                {
                    if (controlACartInventory.Cart.Count > 0)
                    {
                        FindObjectOfType<Core.GameManager>().maxCash += 
                            controlACartInventory.Cart[controlACartInventory.Cart.Count - 1].
                            GetComponent<Control.controlWoods>().WoodCost;
                        controlACartInventory.Cart[controlACartInventory.Cart.Count - 1].GetComponent<Control.controlWoods>().isDestroy = true;
                        controlACartInventory.Cart.Remove(controlACartInventory.Cart[controlACartInventory.Cart.Count - 1]);
                        x = 0.01f;
                    }
                }

                if (controlACartInventory.Cart.Count <= 0)
                    Destroy(this.gameObject);
            }
        }
    }
}

