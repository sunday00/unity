using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Duck.Player
{
    public class Dongle : MonoBehaviour
    {
        public GameManager Manager;

        public ParticleSystem effect;

        public Camera mainCamera;
        public bool isDrag;
        public int level;

        public bool isMerge;

        private Animator _anim;
        private CircleCollider2D _collider;
        private Rigidbody2D _rb;

        private float deadTime;
        private SpriteRenderer sprite;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _collider = GetComponent<CircleCollider2D>();
            sprite = GetComponent<SpriteRenderer>();

            if (mainCamera.IsUnityNull()) mainCamera = Camera.main;
        }

        private void Update()
        {
            if (!isDrag) return;

            // Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            var leftBorder = -3.9f + transform.localScale.x / 2f;
            var rightBorder = 3.9f - transform.localScale.x / 2f;

            if (mousePosition.x < leftBorder) mousePosition.x = leftBorder;
            if (mousePosition.x > rightBorder) mousePosition.x = rightBorder;

            mousePosition.y = 8f;

            transform.position = Vector2.Lerp(transform.position, mousePosition, 0.2f);
        }

        private void OnEnable()
        {
            _anim.SetInteger("Level", level);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.tag != "Dongle") return;

            var other = collision.gameObject.GetComponent<Dongle>();

            if (level != other.level) return;

            if (isMerge) return;

            if (other.isMerge) return;

            if (level >= 7) return;

            var meX = transform.position.x;
            var meY = transform.position.y;
            var otherX = other.transform.position.x;
            var otherY = other.transform.position.y;

            if (meY < otherY || (meY.Equals(otherY) && meX > otherX))
            {
                other.Hide(transform.position);

                LevelUp();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag.Equals("Finish"))
            {
                deadTime += Time.deltaTime;

                if (deadTime > 2) sprite.color = new Color(0.9f, 0.2f, 0.2f, 0.9f);

                if (deadTime > 5) Manager.GameOver();
            }
        }

        public void Hide(Vector2 targetPos)
        {
            isMerge = true;
            _rb.simulated = false;
            _collider.enabled = false;

            StartCoroutine(HideRoutine(targetPos));
        }

        private IEnumerator HideRoutine(Vector2 targetPos)
        {
            var frameCount = 0;

            while (frameCount < 20)
            {
                frameCount++;
                transform.position = Vector2.Lerp(transform.position, targetPos, 0.2f);

                yield return null;
            }

            Manager.score += (int)Mathf.Pow(2, level);

            isMerge = false;
            // TODO: why not destroy?
            gameObject.SetActive(false);
        }

        private void LevelUp()
        {
            isMerge = true;
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0;

            StartCoroutine(LevelUpRoutine());
        }

        private IEnumerator LevelUpRoutine()
        {
            yield return new WaitForSeconds(0.05f);

            _anim.SetInteger("Level", level + 1);

            EffectPlay();

            yield return new WaitForSeconds(0.05f);
            level++;

            Manager.maxLevel = Mathf.Max(level, Manager.maxLevel);

            isMerge = false;
        }

        private void EffectPlay()
        {
            effect.transform.position = transform.position;
            effect.transform.localScale = transform.localScale;
            effect.Play();
        }

        public void Drag()
        {
            isDrag = true;
        }

        public void Drop()
        {
            isDrag = false;
            _rb.simulated = true;
        }
    }
}