using System;
using UnityEngine;

public class CircleProjectile : MonoBehaviour, IProjectile
{

    public int _Damage => _damage;
    public IShapeBehaviour _Creator { get; private set; }

    public Vector2 _MoveDir { get; set; }
    public event Action<CircleProjectile> destroyed;
    
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _moveSpeed = 7;
    
    public void Init(IShapeBehaviour creator)
    {
        _Creator = creator;
    }
    
    private void Update()
    {
        //If is required so the projectiles won't fuck up movement while circling around the shape
        if(_MoveDir.magnitude > 0.1f)
            transform.position = (Vector2)transform.position + _MoveDir.normalized * (_moveSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        ShapeMotor shapeMotor = col.GetComponent<ShapeMotor>();
        IShapeBehaviour shapeBehaviour = col.GetComponent<IShapeBehaviour>();
        IProjectile projectile = col.GetComponent<IProjectile>();

        if (projectile!= null && projectile.GetType() != this.GetType())
        {
            Die();
        }
        //If ShapeMotor is on Object and Shapebehavior is something else than the Type that fired. i.e. triangles can not hit other triangles
        else if (shapeBehaviour != null && shapeBehaviour.GetType() !=  _Creator.GetType())
        {
            shapeMotor._Health -= _Damage;
            Die();
        }
        else if (col.CompareTag("Wall"))
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
        destroyed?.Invoke(this);
    }
}