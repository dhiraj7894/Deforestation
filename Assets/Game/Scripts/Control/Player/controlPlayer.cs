using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Wood.Control
{
    public class controlPlayer : MonoBehaviour
    {
        [Header("Cutter Power")]        
        public float CuttingStrength = 1;
        public float RotationSpeed = 135f;
        public int SpikLevel = 0;

        [Header("Machine Size")]
        [Space(20)]
        public float CutterSize = 0.5f;
        public float CutterPos = 0.147f;
        public int CartLimitData = 19;

        [Header("Machine Limit")]
        [Space(10)]
        public float lCutterSize = 20;
        public float lRotationSpeed = 250;
        public int lSpikLevel = 5;
        public int lCart = 50;

        
        [Header("Machine Control")]
        [Space(20)]
        public float incrementSpeed = 0.1f;
        public float SizeAndPos = 0.05f;
        public int CartUpdateCount;

        [Header("Machine")]
        [Space(20)]
        public Transform Cutter;
        public Transform CutterHolder;
        public Control.controlCutter controlCutter;

        [Header("Machine UI")]
        [Space(20)]
        public Slider sCuttingStrength;
        public Slider sRotationSpeed;
        public Slider sSpikLevel;
        public Slider sCartLimit;
        public Slider sCartLimitIndicator;

        [Header("Mahcine Text")]
        [Space(10)]
        public TextMeshProUGUI tCartLimitIndicator;

        [Header("Cutter")]
        [Space(10)]
        public List<GameObject> CutterLevel = new List<GameObject>();


        private float cutterCurrentSize;
        private float cutterCurrentPos;

        private Core.GameManager GameManager;
        void Start()
        {
            GameManager = FindObjectOfType<Core.GameManager>();
            sCartLimitIndicator.maxValue = CartLimitData;
            cutterCurrentSize = CutterSize;
            cutterCurrentPos = CutterPos;
            LimitSet();
           
        }

        void Update()
        {
            sizeIncreser();
            spikeUpdater();
            StrengthCalculator();
            CutterSizeController(cutterCurrentSize, cutterCurrentPos);
            tCartLimitIndicator.text = sCartLimitIndicator.value + " / " + sCartLimitIndicator.maxValue.ToString("N0");
        }

        void LimitSet()
        {
            sCuttingStrength.maxValue = lCutterSize;
            sRotationSpeed.maxValue = lRotationSpeed;
            sSpikLevel.maxValue = lSpikLevel;
            sCartLimit.maxValue = lCart;


            sCuttingStrength.minValue = sCuttingStrength.value = CutterSize;
            sRotationSpeed.minValue = sRotationSpeed.value = RotationSpeed;
            sSpikLevel.minValue = sSpikLevel.value = SpikLevel;
            sCartLimit.minValue = sCartLimit.value = CartLimitData;
        }
        public void StrengthUpdate()
        {
            if(GameManager.maxCash >= GameManager.Size && CutterSize <= lCutterSize)
            {
                GameManager.maxCash -= GameManager.Size;
                CutterSize += SizeAndPos;
                CutterPos += SizeAndPos;
                sCuttingStrength.value = CutterSize;
            }            
        }
        public void RotationUpdate()
        {
            if (GameManager.maxCash >= GameManager.Rotation && RotationSpeed <= lRotationSpeed)
            {
                GameManager.maxCash -= GameManager.Rotation;
                RotationSpeed += 5;
                sRotationSpeed.value = RotationSpeed;
            }
        }
        public void SpikeUpdate()
        {
            if (GameManager.maxCash >= GameManager.Spike && SpikLevel <= lSpikLevel)
            {
                GameManager.maxCash -= GameManager.Spike;
                SpikLevel += 1;
                sSpikLevel.value = SpikLevel;
            }
        }
        public void CartLimit()
        {
            if(GameManager.maxCash >= GameManager.CartLimit && CartLimitData <= lCart)
            {
                GameManager.maxCash -= GameManager.CartLimit;
                CartLimitData += CartUpdateCount;
                sCartLimitIndicator.maxValue = CartLimitData;
                sCartLimit.value = CartLimitData;
            }           
        }


        void StrengthCalculator()
        {
            CuttingStrength = (CutterSize + CutterPos + (RotationSpeed*0.07f )+ SpikLevel);
        }

        int i = 0;
        void spikeUpdater()
        {
            if (i != SpikLevel)
            {
                CutterLevel[i].SetActive(false);           
                i++;
            }else if (i == SpikLevel)
            {
                CutterLevel[i].SetActive(true);
            }
            if (i >= CutterLevel.Count)
                i = 0;
        }

        void sizeIncreser()
        {
            if(cutterCurrentSize < CutterSize)
            {
                cutterCurrentSize += incrementSpeed * Time.deltaTime;
                if (cutterCurrentSize >= CutterSize)
                    cutterCurrentSize = CutterSize;
            }
            if (cutterCurrentPos < CutterPos)
            {
                cutterCurrentPos += incrementSpeed * Time.deltaTime;
                if (cutterCurrentPos >= CutterPos)
                    cutterCurrentPos = CutterPos;
            }
            controlCutter.RotationSpeed = new Vector3(0, RotationSpeed, 0);
        }

        void CutterSizeController(float size, float pos)
        {
            Cutter.localPosition = new Vector3(Cutter.localPosition.x, Cutter.localPosition.y, pos);
            Cutter.localScale = new Vector3(size, Cutter.localScale.y, size);
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

