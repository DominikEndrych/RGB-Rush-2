using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float SpeedX = .0f;
    public float SpeedY = .0f;
    public float SpeedZ = .0f;

    public bool StartOnAwake = true;

    private Transform _selfTransform;
    private bool _rotate = false;

    private void Awake()
    {
        _selfTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        _rotate = StartOnAwake;
    }

    private void Update()
    {
        if(_rotate)
        {
            Vector3 rotation = new Vector3(SpeedX, SpeedY, SpeedZ) * Time.deltaTime;
            _selfTransform.Rotate(rotation, Space.Self);
        }
        
    }
}
