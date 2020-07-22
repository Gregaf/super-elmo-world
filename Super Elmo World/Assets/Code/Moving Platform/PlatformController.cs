using UnityEngine;

public enum PlatformType
{
    Looping,
    Cyclic
}

public class PlatformController : MonoBehaviour
{
    public PlatformType platformType;

    public float platformSpeed;
    public float platformPauseTime;

    public MovingPlatform movingPlatform;
    public Transform[] path;

    private FSM movingPlatformFsm;

    private void Awake()
    {
        movingPlatform = GetComponentInChildren<MovingPlatform>();
        movingPlatformFsm = new FSM();

        movingPlatformFsm.AddToStateList("Looping", new PlatformBacknForth(movingPlatformFsm, movingPlatform.gameObject, platformSpeed, path));
        movingPlatformFsm.AddToStateList("Cyclic", new PlatformCycle(movingPlatformFsm, movingPlatform.gameObject, platformSpeed, path));
        movingPlatformFsm.AddToStateList("PlatformPause", new PlatformPause(movingPlatformFsm, movingPlatform.gameObject ,platformType, platformPauseTime));

    }

    public void Start()
    {
        movingPlatformFsm.InitializeFSM(movingPlatformFsm.GetState(platformType.ToString()));
             
    }

    private void Update()
    {
        movingPlatformFsm.UpdateCurrentState();

        print(movingPlatformFsm.CurrentState.ToString());
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
