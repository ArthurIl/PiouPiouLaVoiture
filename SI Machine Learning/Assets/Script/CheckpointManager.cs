using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    public Transform firstCheckpoint;

    private void Awake()
    {
        instance = this;
        Init();
    }

    [ContextMenu("Test Init")]
    public void Init()
    {
        firstCheckpoint = transform.GetChild(0);

        for (int x = 0; x < transform.childCount-1; x++)
        {
            transform.GetChild(x).GetComponent<Checkpoint>().nextCheckpoint = transform.GetChild(x + 1);

        }

        transform.GetChild(transform.childCount - 1).GetComponent<Checkpoint>().nextCheckpoint = transform.GetChild(0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
