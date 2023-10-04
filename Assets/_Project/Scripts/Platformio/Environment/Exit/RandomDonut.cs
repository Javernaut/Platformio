using UnityEngine;

namespace Platformio.Environment.Exit
{
    [ExecuteAlways]
    public class RandomDonut : MonoBehaviour
    {
        [Range(0, 100)] [SerializeField] private float torque = 50f;

        [SerializeField] private SpriteRenderer doughRenderer;
        [SerializeField] private SpriteRenderer creamRenderer;
        [SerializeField] private SpriteRenderer toppingRenderer;

        // TODO Consider reading the values directly from Resources instead of loading all of them at once
        [SerializeField] private Sprite[] doughTypes;
        [SerializeField] private Sprite[] creamTypes;
        [SerializeField] private Sprite[] toppingTypes;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            SetRandomSprite(doughRenderer, doughTypes);
            SetRandomSprite(creamRenderer, creamTypes);
            SetRandomSprite(toppingRenderer, toppingTypes);

            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            if (Application.IsPlaying(gameObject)) _rigidbody.AddTorque(torque);
        }

        private void SetRandomSprite(SpriteRenderer renderer, Sprite[] sprites)
        {
            renderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}