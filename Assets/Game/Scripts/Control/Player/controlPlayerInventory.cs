using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stone.control
{
    public class controlPlayerInventory : MonoBehaviour
    {
        public GameObject[] WoodType;
        public List<GameObject> Cart = new List<GameObject>();
        public Transform cartTransform;
        

        [SerializeField] private Vector3 startPoint;
        public bool SellItemInCart;

        private Transform SellPoint = default;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (SellItemInCart) 
                SellCartItem();
        }

        float x = 0.05f;
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
                    Cart[Cart.Count - 1].transform.GetComponent<controlWoods>().speed += 5;
                    Cart[Cart.Count - 1].transform.GetComponent<controlWoods>().startMove = true;
                    Cart.Remove(Cart[Cart.Count - 1]);
                    x = 0.05f;
                }                
            }
        }
        public void CartArrangement()
        {
            if (Cart.Count == 1)
            {
                Cart[0].transform.GetComponent<controlWoods>().endPos = startPoint;
                return;
            }
            if(Cart.Count > 1)
            {
                if (Cart.Count > 1 && Cart.Count % 3 != 0 && Cart.Count % 9 != 0)
                {
                    Cart[Cart.Count - 1].GetComponent<controlWoods>().endPos =
                            new Vector3(Cart[Cart.Count - 2].GetComponent<controlWoods>().endPos.x - (-0.235f),
                            Cart[Cart.Count - 2].GetComponent<controlWoods>().endPos.y,
                            Cart[Cart.Count - 2].GetComponent<controlWoods>().endPos.z);
                }
                if (Cart.Count > 1 && Cart.Count % 3 == 0 && Cart.Count % 9 != 0)
                {
                    Cart[Cart.Count - 1].GetComponent<controlWoods>().endPos =
                            new Vector3(startPoint.x,
                            Cart[Cart.Count - 2].GetComponent<controlWoods>().endPos.y,
                            Cart[Cart.Count - 2].GetComponent<controlWoods>().endPos.z - 0.215f);
                }
                if (Cart.Count > 1 && Cart.Count % 3 == 0 && Cart.Count % 9 == 0)
                {
                    Cart[Cart.Count - 1].GetComponent<controlWoods>().endPos =
                            new Vector3(startPoint.x,
                            Cart[Cart.Count - 2].GetComponent<controlWoods>().endPos.y + 0.2f,
                            startPoint.z);
                }
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
                moveObjectToCart(other);
                CartArrangement();                
            }
          
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Sell"))
            {
                SellPoint = GameObject.Find("SellPoint").transform;

                if (GetComponent<Stone.movement.movePlayer>().direction.magnitude <= 0.1f)
                    SellItemInCart = true;
                if(GetComponent<Stone.movement.movePlayer>().direction.magnitude >= 0.1f)
                    SellItemInCart = false;
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
