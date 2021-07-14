using UnityEngine;
using SaveData;

namespace BoidsSimulationOnGPU
{
    public class CameraOperate : MonoBehaviour
    {
        [Header("Move Speed")]
        [Range(0f, 2f)] float scrollSpeed = 1f;
        [Range(0f, 2f)] float rotateXSpeed = 1f;
        [Range(0f, 2f)] float rotateYSpeed = 1f;
        [Range(0f, 2f)] float moveSpeed = 1f;
        [Range(0f, 20f)] float keyMoveSpeed = 1f;

        //Whether currently in rotation
        private bool isRotate = false;

        //Is currently in panning
        private bool isMove = false;

        //Camera transform component cache
        private Transform tr;

        //The initial position of the camera at the beginning of the operation
        private Vector3 traStart;

        //The initial position of the mouse as the camera begins to operate
        private Vector3 mouseStart;

        //Is the camera facing down
        private bool isDown = false;

        public void GetSettingData(PlayerSetting data)
        {
            scrollSpeed = data.scrollSpeed;
            rotateXSpeed = data.rotateXSpeed;
            rotateYSpeed = data.rotateYspeed;
            moveSpeed = data.moveSpeed;
            keyMoveSpeed = data.keyMoveSpeed;
        }

        void Start()
        {
            tr = transform;
        }

        void Update()
        {
            //When in the rotation state, and the right mouse button is released, then exit the rotation state
            if (isRotate && Input.GetMouseButtonUp(1))
            {
                isRotate = false;
            }
            //When it is in the translation state, and the mouse wheel is released, it will exit the translation state
            if (isMove && Input.GetMouseButtonUp(2))
            {
                isMove = false;
            }

            float speed = keyMoveSpeed;
            // press LeftShift to make speed *2
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 2f * speed;
            }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                tr.position += tr.forward * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                tr.position -= tr.forward * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                tr.position -= tr.right * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                tr.position += tr.right * Time.deltaTime * speed;
            }

            if (isRotate)
            {
                //Gets the offset of the mouse on the screen
                Vector3 offset = Input.mousePosition - mouseStart;

                // whether the lens is facing down
                if (isDown)
                {
                    // the final rotation Angle = initial Angle + offset, 0.3f coefficient makes the rotation speed normal when rotateYSpeed, rotateXSpeed is 1
                    tr.rotation = Quaternion.Euler(traStart + new Vector3(offset.y * 0.3f * rotateYSpeed, -offset.x * 0.3f * rotateXSpeed, 0));
                }
                else
                {
                    // final rotation Angle = initial Angle + offset
                    tr.rotation = Quaternion.Euler(traStart + new Vector3(-offset.y * 0.3f * rotateYSpeed, offset.x * 0.3f * rotateXSpeed, 0));
                }
            }
            // press the right mouse button to enter the rotation state
            else if (Input.GetMouseButtonDown(1) && !isMove)
            {
                isRotate = true;
                mouseStart = Input.mousePosition;
                traStart = tr.rotation.eulerAngles;
                // to determine whether the lens is facing down (the Y-axis is <0 according to the position of the object facing up),-0.0001f is a special case when x rotates 90
                isDown = tr.up.y < -0.0001f ? true : false;
            }

            // whether it is in the translation state
            if (isMove)
            {
                Vector3 offset = Input.mousePosition - mouseStart;
                tr.position = traStart + tr.up * -offset.y * 0.1f * moveSpeed + tr.right * -offset.x * 0.1f * moveSpeed;
            }
            // click the mouse wheel to enter translation mode
            else if (Input.GetMouseButtonDown(2) && !isRotate)
            {
                isMove = true;
                mouseStart = Input.mousePosition;
                traStart = tr.position;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                tr.position += tr.forward * scroll * 1000f * Time.deltaTime * scrollSpeed;
            }
        }
    }
}