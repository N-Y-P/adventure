using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace newscene
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager instance;

        [Header("====¿Œ«≤====")]
        [Header("WASD")]
        [SerializeField] Vector3 dir;

        private void Awake()
        {
            /*
            if (InputManager.instance != null)
                Destroy(gameObject);
            */

            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            dir = Input_WASD();
        }

        Vector3 Input_WASD()
        {
            float ver = Input.GetAxis("Vertical");
            float hor = Input.GetAxis("Horizontal");

            return new Vector3(hor, 0, ver);
        }



        #region »£√‚øÎ

        public Vector3 Get_Direction()
        {
            return dir;
        }

        #endregion

    }
}