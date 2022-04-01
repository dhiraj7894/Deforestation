using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stone.Control
{
    public class controlPlayerInventory : MonoBehaviour
    {
        public GameObject[] WoodType;
        public List<GameObject> Cart = new List<GameObject>();
        public Transform cartTransform;
        public Vector2 StackArray = new Vector2(3, 8);

        [SerializeField] private Vector3 startPoint;
        public bool SellItemInCart;

        private Transform SellPoint = default;
        private controlPlayer controlPlayer;
        // Start is called before the first frame update
        void Start()
        {
            controlPlayer = GetComponent<controlPlayer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (SellItemInCart) 
                SellCartItem();
            if(Cart.Count>0)
                cartLimitUpdator();
        }

        float x = 0.01f;
        public void SellCartItem()
        {
            if (SellItemInCart && Cart.Count>0)
            {
                if (x > 0)
                    x -= Time.deltaTime;
                if (x <= 0)
                {
                    Cart[Cart.Count - 1].transform.parent = null;
                    Cart[Cart.Count - 1].transform.GetComponent<controlWoods>().endPos = SellPoint.position;
                    Cart[Cart.Count - 1].transform.GetComponent<controlWoods>().moveSpeed += 35;
                    Cart[Cart.Count - 1].transform.GetComponent<controlWoods>().startMove = true;
                    Cart.Remove(Cart[Cart.Count - 1]);
                    x = 0.05f;
                }                
            }
        }

        public void cartLimitUpdator()
        {
            if(Cart.Count - 1 > controlPlayer.sCartLimitIndicator.value)
            {
                controlPlayer.sCartLimitIndicator.value += 100 * Time.deltaTime;
                if (controlPlayer.sCartLimitIndicator.value >= Cart.Count - 1)
                    controlPlayer.sCartLimitIndicator.value = Cart.Count - 1;
            }
            if (Cart.Count - 1 < controlPlayer.sCartLimitIndicator.value)
            {
                controlPlayer.sCartLimitIndicator.value -= 1000 * Time.deltaTime;
                if (controlPlayer.sCartLimitIndicator.value <= Cart.Count - 1)
                    controlPlayer.sCartLimitIndicator.value = Cart.Count - 1;
            }
        }

        public void CartArrangement()
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

        public void moveObjectToCart(Collider other)
        {
            other.transform.parent = cartTransform;            
            if (!Cart.Contains(other.gameObject))
                Cart.Add(other.gameObject);
            other.transform.GetComponent<controlWoods>().startMove = true;
            return;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Woods"))
            {
                if (Cart.Count <= controlPlayer.CartLimitData)
                {
                    moveObjectToCart(other);
                    CartArrangement();
                }                
            }
          
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Sell"))
            {
                SellPoint = GameObject.Find("SellPoint").transform;
                SellItemInCart = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Sell"))
            {
                SellPoint = null;
                SellItemInCart = false;
            }
        }
    }
}
