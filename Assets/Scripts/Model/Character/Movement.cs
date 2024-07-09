using UnityEngine;

namespace BGS
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] float speed = 4f;
        [SerializeField] Rigidbody2D rigidb;
        Vector2 movement;

        private void Start()
        {
            rigidb = GetComponent<Rigidbody2D>();
        }

        public void MoveCharacter(float horizontal, float vertical, float deltaTime)
        {
            movement.x = horizontal;
            movement.y = vertical;

            rigidb.MovePosition(rigidb.position + movement * speed * deltaTime);
        }
    }
}

