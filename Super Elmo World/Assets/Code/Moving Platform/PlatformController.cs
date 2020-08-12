using UnityEngine;

public enum PlatformType
{
    Looping,
    Cyclic
}

public class PlatformController : MonoBehaviour
{
    public PlatformType platformType;

    [SerializeField] private float platformSpeed = 5f;
    [SerializeField] private float platformPauseTime = 1f;

    [SerializeField] private Transform[] path = null;
    private MovingPlatform movingPlatform;

    private FSM movingPlatformFsm;

    private void Awake()
    {
        movingPlatform = GetComponentInChildren<MovingPlatform>();
        movingPlatformFsm = new FSM();

        movingPlatformFsm.AddToStateList("Looping", new PlatformBacknForth(movingPlatformFsm, movingPlatform.gameObject, platformSpeed, path));
        movingPlatformFsm.AddToStateList("Cyclic", new PlatformCycle(movingPlatformFsm, movingPlatform.gameObject, platformSpeed, path));
        movingPlatformFsm.AddToStateList("PlatformPause", new PlatformPause(movingPlatformFsm, movingPlatform.gameObject ,platformType, platformPauseTime));

    }

    private void Start()
    {
        movingPlatformFsm.InitializeFSM(movingPlatformFsm.GetState(platformType.ToString()));
             
    }

    private void Update()
    {
        movingPlatformFsm.UpdateCurrentState();

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        int listLength = path.Length;

        for (int i = 0; i < listLength; i++)
        {
            if (i == (listLength - 1))
                continue;

            Gizmos.DrawLine(path[i].position, path[i + 1].position);
        }

    }
}
