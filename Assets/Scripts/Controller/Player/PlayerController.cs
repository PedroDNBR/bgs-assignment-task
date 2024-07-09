using UnityEngine;

namespace BGS
{
    public class PlayerController : MonoBehaviour
    {
        Movement movement;

        float vertical;
        float horizontal;

        void Start()
        {
            movement = GetComponent<Movement>();
        }

        private void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        void FixedUpdate()
        {
            movement.MoveCharacter(horizontal, vertical, Time.fixedDeltaTime);
        }
    }
}

