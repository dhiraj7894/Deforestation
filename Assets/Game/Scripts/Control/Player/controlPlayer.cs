using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stone.control
{
    public class controlPlayer : MonoBehaviour
    {
        public float CuttingStrength = 1;

        public float CutterSize = 0.5f;
        public float CutterPos = 0.147f;
        public float incrementSpeed = 1;
        public Transform Cutter;
        public Transform CutterHolder;

        void Start()
        {
            
        }

        void Update()
        {
            CutterSizeController(CutterSize, CutterPos);
/*            if (Input.GetMouseButton(0))
            {
                CutterSize += incrementSpeed * Time.deltaTime;
                CutterPos += incrementSpeed * Time.deltaTime;
            }*/
        }
        void CutterSizeController(float size, float pos)
        {
            Cutter.localPosition = new Vector3(Cutter.localPosition.x, Cutter.localPosition.y, pos);
            Cutter.localScale = new Vector3(CutterSize, Cutter.localScale.y, size);
            CutterHolder.localScale = new Vector3(CutterHolder.localScale.x, CutterHolder.localScale.y, size);
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Mineral"))
            {
                try
                {
                    other.gameObject.GetComponent<controlMineral>().Health -= CuttingStrength * Time.deltaTime;
                }
                catch
                {
                    print("NO SCRIPT FOUND !");
                }
            }
        }
        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.CompareTag("Mineral"))
            {
                try
                {
                    other.gameObject.GetComponent<controlMineral>().Health -= CuttingStrength * Time.deltaTime;
                }
                catch
                {
                    print("NO SCRIPT FOUND !");
                }
            }
        }
    }
}

