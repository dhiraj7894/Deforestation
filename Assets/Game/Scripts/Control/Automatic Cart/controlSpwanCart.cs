using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wood.Control
{
    public class controlSpwanCart : MonoBehaviour
    {
        public GameObject ACart;
        public GameObject CartObject;
        public Transform SpwanPosition;
        public controlACartPathPoints controlACartPathPoints;
        public controlACartInventory controlACartInventory;
        public bool isCartUnlocked = false;
        public GameObject Locked;
        public GameObject Unlocked;
        public GameObject UI;
        private void Update()
        {
            if(isCartUnlocked)
                SpwanCart();

            if (isCartUnlocked)
            {
                Unlocked.SetActive(true);
                Locked.SetActive(false);
            }
            if (!isCartUnlocked)
            {
                Unlocked.SetActive(false);
                Locked.SetActive(true);
            }
        }
        void SpwanCart()
        {
            if (ACart == null)
            {
                GameObject cart = Instantiate(CartObject, transform);
                ACart = cart;
                ACart.transform.position = SpwanPosition.position;
                ACart.transform.rotation = Quaternion.Euler(0, 180, 0);
                ACart.GetComponent<Move.moveACart>().controlACartPathPoints = controlACartPathPoints;
                ACart.GetComponent<Move.moveACart>().controlACartInventory = controlACartInventory;
                controlACartInventory.inventory = ACart.GetComponent<Move.moveACart>().inventory;
            }
        }
        public void Unlock()
        {
            if (!isCartUnlocked)
                isCartUnlocked = true;
            UI.GetComponent<Animator>().Play("Exit");
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !isCartUnlocked)
            {
                UI.SetActive(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (UI.activeSelf)
                UI.GetComponent<Animator>().Play("Exit");
        }
    }
}

