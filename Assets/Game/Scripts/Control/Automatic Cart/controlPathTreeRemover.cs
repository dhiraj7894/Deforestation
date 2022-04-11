using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Wood.Control
{
    public class controlPathTreeRemover : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Mineral"))
            {
                try
                {
                    other.gameObject.GetComponent<controlMineral>().isRemove = true;
                }
                catch
                {
                    print("NO SCRIPT FOUND !");
                }
            }
        }
    }
}

