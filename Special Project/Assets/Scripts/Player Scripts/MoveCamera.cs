using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform player;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update() {
        transform.position = player.transform.position;
    }
}
