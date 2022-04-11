using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wood.Control
{
    public class controlProgressBar : MonoBehaviour
    {
        public Image[] Stars;

        [Space(15)]
        public Color BaseColor;
        public Color CompletedColor;

        public Core.LevelProgress LevelProgress;
        void Start()
        {

        }

        
        void Update()
        {
            StartCalculation();
        }

        void StartCalculation()
        {
            if (LevelProgress != null)
            {
                if(LevelProgress.ProgressBar.value >= (LevelProgress.ProgressBar.maxValue * 50 * 0.01))
                {
                    Stars[0].GetComponentInParent<Animator>().Play("popup");
                    Stars[0].color = CompletedColor;
                }
                if (LevelProgress.ProgressBar.value >= (LevelProgress.ProgressBar.maxValue * 70 * 0.01))
                {
                    Stars[1].GetComponentInParent<Animator>().Play("popup");
                    Stars[1].color = CompletedColor;
                }
                if (LevelProgress.ProgressBar.value >= LevelProgress.ProgressBar.maxValue)
                {
                    Stars[2].GetComponentInParent<Animator>().Play("popup");
                    Stars[2].color = CompletedColor;
                }
            }
        }
    }
}

