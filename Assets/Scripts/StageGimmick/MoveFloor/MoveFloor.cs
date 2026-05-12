using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    [SerializeField] private float maxPosY = 0;

    [SerializeField] private float minPosY = 0;
    void Start()
    {
        
    }

    void Update()
    {
        ClampYPosition();
    }

    // ˆÚ“®‚Å‚«‚é”ÍˆÍ‚ğ§ŒÀ
    private void ClampYPosition()
    {
        Vector3 pos = transform.localPosition;
        pos.y = Mathf.Clamp(pos.y, minPosY, maxPosY);
        transform.localPosition = pos;
    }
}
