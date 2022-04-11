using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wood.Core
{
    public class LevelManager : MonoBehaviour
    {
        public int LevelNumber = 0;

        public List<GameObject> Level = new List<GameObject>();
        public List<Vector3> spwanPosition = new List<Vector3>();
        public List<Vector3> movePosition = new List<Vector3>();

        public GameObject LevelClone;
        void Start()
        {

        }

        
        void Update()
        {
            Spwanner();
        }
        void Spwanner()
        {
            if (LevelClone == null)
            {
                GameObject Lv = Instantiate(Level[LevelNumber], transform);
                LevelClone = Lv;
                Lv.transform.position = spwanPosition[LevelNumber];
                Lv.GetComponent<LevelProgress>().toInTheGame = movePosition[LevelNumber];
                Lv.GetComponent<LevelProgress>().isMoveToGame = true;

            }
        }
    }
}

