using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stone.Control
{
    public class controlMineral : MonoBehaviour
    {
        public float Health;
        public bool isMineralDown;
        public float FallingTime=0.5f;
        public GameObject Partical;
        public GameObject Mineral;
        public Transform DC;
        public LayerMask L;

        private Transform Player;

        private void Start()
        {
            Player = GameObject.Find("Player").transform;
        }
        private void Update()
        {
            if (Health <= 0)
            {
                if(transform.GetComponent<Rigidbody>() == null)
                    this.gameObject.AddComponent<Rigidbody>();                
                GetComponent<Rigidbody>().AddForce(Player.forward * 0.1f, ForceMode.Impulse);
                this.gameObject.layer = 9;
                StartCoroutine(SpwnObjects(FallingTime));

                /*                M.GetComponent<Rigidbody>().AddForce(M.transform.up *5, ForceMode.Impulse);
                                M.GetComponent<Rigidbody>().AddForce(M.transform.right * 5, ForceMode.Impulse);*/
            }
         /*   if (!isMineralDown)
            {
                if(transform.GetComponent<Rigidbody>() == null)
                    this.gameObject.AddComponent<Rigidbody>();
            }

            if (isMineralDown)
            {
                if(transform.GetComponent<Rigidbody>() != null)
                {
                    Destroy(transform.GetComponent<Rigidbody>());
                }
            }
            isMineralDown = Physics.CheckSphere(DC.position, 0.01f, L);*/
        }   



        IEnumerator SpwnObjects(float t)
        {
            yield return new WaitForSeconds(t);
            Destroy(this.gameObject);
            GameObject M = Instantiate(Mineral, transform.position, Quaternion.Euler(10, 50, 65), GameObject.Find("Mineral").transform);
            Destroy(Instantiate(Partical, transform.position, Quaternion.identity, GameObject.Find("Mine Partical").transform), 1f);
        }
    }
}
