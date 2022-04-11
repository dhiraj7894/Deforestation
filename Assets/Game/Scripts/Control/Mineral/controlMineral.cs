using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wood.Control
{
    public class controlMineral : MonoBehaviour
    {
        public float Health;
        public bool isMineralDown;
        public bool isRemove;
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
            if (Health <= 0 && !isRemove)
            {
                if(transform.GetComponent<Rigidbody>() == null)
                    this.gameObject.AddComponent<Rigidbody>();
                GetComponent<Rigidbody>().angularDrag = 0;
                GetComponent<Rigidbody>().AddForce(Player.forward * 0.05f, ForceMode.Impulse);                
                this.gameObject.layer = 9;
                StartCoroutine(SpwnObjects(FallingTime));
            }
            if (isRemove)
            {
                if (transform.GetComponent<Rigidbody>() == null)
                    this.gameObject.AddComponent<Rigidbody>();                
                this.gameObject.layer = 9;
                StartCoroutine(SpwnObjects(FallingTime));
            }
        }   

        IEnumerator SpwnObjects(float t)
        {
            yield return new WaitForSeconds(t);
            GameObject M = Instantiate(Mineral, transform.position, Quaternion.Euler(10, 50, 65), GameObject.Find("Mineral").transform);
            Destroy(this.gameObject);
            Destroy(Instantiate(Partical, transform.position, Quaternion.identity, GameObject.Find("Mine Partical").transform), 1f);
        }
    }
}
