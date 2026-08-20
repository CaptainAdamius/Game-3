using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public enum EnemyState { Idle, Attack, Dead }
    public EnemyState enemyState;



    [SerializeField] float rayDistance;
    [SerializeField] LayerMask hitLayer;
    [SerializeField] Transform rayStartPos;


    public float facingDir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StateHandle();
        PlayerCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StateHandle()
    {

        switch (enemyState)
        {
            case EnemyState.Idle:

                break;

            case EnemyState.Attack:

                break;
            case EnemyState.Dead:


                break;
        }
    }


    private void PlayerCheck()
    {

        RaycastHit2D hit;
        hit = Physics2D.Raycast(rayStartPos.position, facingDir * transform.right, rayDistance, hitLayer);
        if (hit)
        {
            enemyState = EnemyState.Attack;
        }
        else
        {
            enemyState = EnemyState.Idle;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayStartPos.position, facingDir * transform.right * rayDistance);
    }


  

}
