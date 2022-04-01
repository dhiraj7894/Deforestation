using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stone.Control
{
    public class controlPlayerUpgrades : MonoBehaviour
    {
        public GameObject UpgradeUI;

        public bool isPlayerOnRack;
        void Start()
        {

        }

        
        void Update()
        {
            UIManage();
        }

        void UIManage()
        {
            if (isPlayerOnRack)
            {
                UpgradeUI.SetActive(true);
            }
        }
        public void Reset()
        {
            isPlayerOnRack = false;
            UpgradeUI.GetComponent<Animator>().Play("Exit");
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("upgrade") && !isPlayerOnRack)
            {
                isPlayerOnRack = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("upgrade"))
            {
                if(UpgradeUI.activeSelf)
                    UpgradeUI.GetComponent<Animator>().Play("Exit");

                isPlayerOnRack = false;
            }
        }
    }
}

