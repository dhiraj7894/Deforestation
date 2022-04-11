using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wood.Control
{
    public class controlACartInventory : MonoBehaviour
    {
        public controlSpwanCart controlSpwanCart;

        public List<GameObject> Cart = new List<GameObject>();
        public Transform inventory;


        public Vector3 startPoint;
        public Vector2 StackArray;

        public float cartUpdateSpeed=0.1f;
        public int CartLimit;

        private controlPlayerInventory controlPlayerInventory;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Cart.Count > 0)
                ArrangeObjectInCart();

            if(controlSpwanCart.isCartUnlocked && 
                Cart.Count <= CartLimit &&
                controlSpwanCart.ACart != null &&
                !controlSpwanCart.ACart.GetComponent<Move.moveACart>().isMove)
                moveObjectToThisCart();

            if (controlSpwanCart.ACart!=null)
                startMove();
        }
        public void ArrangeObjectInCart()
        {
            if (Cart.Count <= 1)
            {
                Cart[Cart.Count - 1].transform.GetComponent<controlWoods>().endPos = startPoint;
                return;
            }
            if (Cart.Count > 1 && Cart.Count % StackArray.x != 0 && Cart.Count % StackArray.y != 0)
            {
                Cart[Cart.Count - 1].GetComponent<controlWoods>().endPos =
                        new Vector3(Cart[Cart.Count - 2].GetComponent<controlWoods>().endPos.x + (0.235f),
                        Cart[Cart.Count - 2].GetComponent<controlWoods>().endPos.y,
                        Cart[Cart.Count - 2].GetComponent<controlWoods>().endPos.z);
                return;
            }
            if (Cart.Count > 1 && Cart.Count % StackArray.x == 0 && Cart.Count % StackArray.y != 0)
            {
                Cart[Cart.Count - 1].GetComponent<controlWoods>().endPos =
                        new Vector3(startPoint.x,
                        Cart[Cart.Count - 2].GetComponent<controlWoods>().endPos.y,
                        Cart[Cart.Count - 2].GetComponent<controlWoods>().endPos.z - 0.215f);
                return;
            }
            if (Cart.Count > 1 && Cart.Count % StackArray.x == 0 && Cart.Count % StackArray.y == 0)
            {
                Cart[Cart.Count - 1].GetComponent<controlWoods>().endPos =
                        new Vector3(startPoint.x,
                        Cart[Cart.Count - 2].GetComponent<controlWoods>().endPos.y + 0.2f,
                        startPoint.z);
                return;
            }
        }

        float x = 0;
        public void moveObjectToThisCart()
        {
            if (controlPlayerInventory)
            {
                if (x > 0)
                    x -= Time.deltaTime;
                if (x <= 0)
                {
                    if (controlPlayerInventory.Cart.Count > 0)
                    {
                        Cart.Add(controlPlayerInventory.Cart[controlPlayerInventory.Cart.Count - 1]);
                        controlPlayerInventory.Cart.Remove(controlPlayerInventory.Cart[controlPlayerInventory.Cart.Count - 1]);
                        Cart[Cart.Count - 1].transform.parent = inventory;
                        Cart[Cart.Count - 1].transform.GetComponent<controlWoods>().moveSpeed = 30;
                        Cart[Cart.Count - 1].transform.GetComponent<controlWoods>().startMove = true;
                        x = cartUpdateSpeed;
                        return;
                    }
                    else
                    {
                        print("Cart is empty");
                    }
                }
                if(Cart.Count>0 && controlPlayerInventory.Cart.Count <= 0 )
                {
                    controlSpwanCart.ACart.GetComponent<Move.moveACart>().isMove = true;
                }
            } 

        }
        void startMove()
        {
            if (Cart.Count > CartLimit)
                controlSpwanCart.ACart.GetComponent<Move.moveACart>().isMove = true;
            if (Cart.Count > 0 && !controlPlayerInventory)
                controlSpwanCart.ACart.GetComponent<Move.moveACart>().isMove = true;
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                try
                {
                    controlPlayerInventory = other.GetComponent<controlPlayerInventory>();
                }
                catch
                {

                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (controlPlayerInventory!=null)
                controlPlayerInventory = null;
        }
    }
}

