using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wood.Core
{
    public class LevelProgress : MonoBehaviour
    {
        public List<GameObject> Tree = new List<GameObject>();
        public Slider ProgressBar;
        [Header("Movement")]
        [Space(5)]
        public Vector3 toInTheGame;
        public Vector3 toOutTheGame;
        
        public bool isMoveToGame;
        public bool isMoveOutGame;

        public float speedOfMovement;

        void Start()
        {
            updateProgressBar();
        }

        void updateProgressBar()
        {
            if (ProgressBar == null)
            {
                ProgressBar = GameObject.Find("ProgressBar").GetComponent<Slider>();
                ProgressBar.GetComponent<Control.controlProgressBar>().LevelProgress = this.GetComponent<LevelProgress>();
                ProgressBar.maxValue = Tree.Count - 1;
            }
        }
        void Update()
        {
            clear();
            updateProgressBarValue();
            Move();
        }

        public void updateProgressBarValue()
        {
            if(ProgressBar != null)
            {
                ProgressBar.value = ProgressBar.maxValue - Tree.Count;
            }
        }

        int i = 0;
        void clear()
        {
            if (i <= Tree.Count - 1)
            {
                if (Tree[i] == null)
                    Tree.Remove(Tree[i]);
                i++;
            }
            else if (i >= Tree.Count - 1)
                i = 0;
        }

        void Move()
        {
            if (isMoveToGame && transform.position != toInTheGame)
            {
                transform.position = Vector3.MoveTowards(transform.position, toInTheGame, speedOfMovement * Time.deltaTime);
                if(transform.position == toInTheGame)
                {
                    FindObjectOfType<GameManager>().Bridge.GetComponent<Animator>().SetBool("landOpen", true);
                    isMoveToGame = false;
                }
            }
            if(isMoveOutGame && transform.position != toOutTheGame)
            {
                FindObjectOfType<GameManager>().Bridge.GetComponent<Animator>().SetBool("landOpen", false);
                transform.position = Vector3.MoveTowards(transform.position, toOutTheGame, (speedOfMovement*2) * Time.deltaTime);
                if(transform.position == toOutTheGame)
                {
                    
                    isMoveToGame = false;
                }
                    
            }
        }
    }

}
