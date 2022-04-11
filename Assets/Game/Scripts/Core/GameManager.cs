using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Wood.Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Cash")]
        public float maxCash;
        public float currentCash;
        public float cashCounterSpeed;
        public TextMeshProUGUI Cash;

        [Header("Upgrade Price")]
        [Space(20)]
        public float Size = 20f;
        public TextMeshProUGUI pSize;
        [Space(5)]
        public float Spike = 250f;
        public TextMeshProUGUI pSpike;
        [Space(5)]
        public float CartLimit = 100f;
        public TextMeshProUGUI pCartLimit;
        [Space(5)]
        public float Rotation = 35f;
        public TextMeshProUGUI pRotation;

        [Space(10)]
        public GameObject Bridge;
        void Start()
        {
            currentCash = maxCash;
        }


        void Update()
        {
            cashCounter();
            PriceTextUpdated();
        }

        void PriceTextUpdated()
        {
            pSize.text = "$" + Size.ToString("N0");
            pSpike.text = "$" + Spike.ToString("N0");
            pCartLimit.text = "$" + CartLimit.ToString("N0");
            pRotation.text = "$" + Rotation.ToString("N0");
        }
        void cashCounter()
        {
            if (maxCash > currentCash)
            {
                currentCash += cashCounterSpeed * Time.deltaTime;
                if (currentCash >= maxCash)
                    currentCash = maxCash;
            }
            if (maxCash < currentCash)
            {
                currentCash -= (cashCounterSpeed * 10) * Time.deltaTime;
                if (currentCash <= maxCash)
                    currentCash = maxCash;
            }
            Cash.text = "$"+currentCash.ToString("N0");
        }
    }
}

