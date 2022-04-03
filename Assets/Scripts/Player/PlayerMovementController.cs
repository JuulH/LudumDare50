using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private GameObject weaponAttachment;
        private Camera _cam;


        public float speed;
        public bool isControlsEnabled = true;

        private Animator _animator;

        private Rigidbody2D _rb;
        public Vector2 dir;
        private Vector2 _mousePosition;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            dir = Vector2.zero;
            _cam = Camera.main;
        }

        private void Update()
        {
            if (isControlsEnabled)
            {
                ControlWeaponAim();

                dir = Vector2.zero;
                if (Input.GetKey(KeyCode.A))
                {
                    dir.x = -1;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    dir.x = 1;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    dir.y = 1;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    dir.y = -1;
                }
            }

            FlipSpritesBasedOnDirFacing();

            dir.Normalize();
            if (dir == Vector2.zero)
            {
                _animator.SetBool("IsIdle", true);
                _animator.SetBool("IsMoving", false);
            }
            else
            {
                _animator.SetBool("IsIdle", false);
                _animator.SetBool("IsMoving", true);
            }

            _rb.velocity = speed * dir;
        }

        private bool hasFlippedRight = true;
        private bool hasFlippedLeft = true;
        
        private void FlipSpritesBasedOnDirFacing()
        {
            Vector2 mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            Vector3 scale = transform.localScale;
            if (mouse.x < Screen.width / 2)
            {
                transform.localScale = new Vector2(-1, scale.y);
            }
            else if (mouse.x > Screen.width / 2)
            {
                transform.localScale = new Vector2(1, scale.y);
            }
        }

        private void ControlWeaponAim()
        {
            _mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 weaponLookDir = _mousePosition - _rb.position;
            float angle = Mathf.Atan2(weaponLookDir.y, weaponLookDir.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            weaponAttachment.transform.rotation = rotation;
        }
    }
}