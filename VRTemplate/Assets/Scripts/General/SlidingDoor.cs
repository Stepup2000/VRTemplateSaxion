using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public enum Direction
    {
        Forward,
        Backward,
        Left,
        Right,
        Up,
        Down          
    }

    public float maxSlideDistance = 5f; // Set the maximum slide distance in the Inspector
    public Direction slideDirection; // Set the slide direction in the Inspector

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isOpen = false;
    public float slidingSpeed = 0.5f; // Adjust the default speed of door movement

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition; // Start with the door closed position
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;

        if (isOpen)
            targetPosition = CalculateTargetPosition(maxSlideDistance);
        else
            targetPosition = initialPosition; // Close the door
    }

    private Vector3 CalculateTargetPosition(float distance)
    {
        Vector3 directionVector = Vector3.zero;

        switch (slideDirection)
        {
            case Direction.Forward:
                directionVector = transform.forward;
                break;
            case Direction.Backward:
                directionVector = -transform.forward;
                break;
            case Direction.Left:
                directionVector = -transform.right;
                break;
            case Direction.Right:
                directionVector = transform.right;
                break;
            case Direction.Up:
                directionVector = transform.up;
                break;
            case Direction.Down:
                directionVector = -transform.up;
                break;
        }

        float clampedDistance = Mathf.Clamp(distance, 0f, maxSlideDistance);
        return initialPosition + directionVector * clampedDistance;
    }

    private void Update()
    {
        float step = slidingSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
