using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _collider;
    public int Size { get => _size; }
    [SerializeField] private int _size;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent<Fruit>(out var fruit);
        if (fruit != null && _size == fruit.Size)
        {
            Debug.Log("Collided");
            Destory();
        }
    }
    private void Destory()
    {
        GameObject.Destroy(this.gameObject);
    }
}
