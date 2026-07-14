using UnityEngine;

public class GoalFlag : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Ball))
        {
            GameManager.Instance.ChangeState(GameState.GameClear);
        }
    }
}
