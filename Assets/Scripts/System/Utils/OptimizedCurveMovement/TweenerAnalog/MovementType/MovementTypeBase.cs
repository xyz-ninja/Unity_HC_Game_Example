using UnityEngine;

public abstract class MovementTypeBase
{
    [field: SerializeField] public float Speed { get; set; } = 5f;

    protected Vector3 InitialPosition { get; set; }
    protected float Distance { get; set; }
    protected float StepScale { get; set; }
    protected float Progress { get; set; }
        
    public void Init(Vector3 initialPosition, Vector3 targetPosition)
    {
        InitialPosition = initialPosition;
        Distance = Vector3.Distance(initialPosition, targetPosition);

        // This is one divided by the total flight duration, to help convert it to 0-1 progress.
        StepScale = Speed / Distance;
        Progress = 0f;
            
        OnInit();
    }

    public abstract void OnInit();

    public abstract Vector3 NextPosition(Vector3 targetPosition, float delta);
}
