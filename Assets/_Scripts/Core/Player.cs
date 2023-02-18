using UnityEngine;

namespace KitchenSimulator.Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;

        private void Update()
        {
            var inputVector = new Vector2(0, 0);
            
            if (Input.GetKey(KeyCode.W))
            {
                inputVector.y = + 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                inputVector.y = -1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                inputVector.x = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                inputVector.x = +1;
            }

            inputVector = inputVector.normalized;

            var moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
            var thisTransform = transform;
            
            thisTransform.position += moveDirection * (_moveSpeed * Time.deltaTime);

            var rotateSpeed = 10f;
            thisTransform.forward = Vector3.Slerp(thisTransform.forward, moveDirection,  rotateSpeed * Time.deltaTime);
            
            Debug.Log($"{inputVector}");
        }
    }
}