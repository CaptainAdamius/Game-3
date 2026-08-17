using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Destrucible : MonoBehaviour
{

    /*
     This script will be attached to all objects that can be destroyed. On destroy, different objects should have different effects:
    - A crate should do nothing.
    - An explosive barrel should explode, dealing damage around it.
    Other ideas include:
    - A weapons cabinet, which should drop a weapon (randomized or predetermined).
    - An indestructible, which does not run the destroy command.
     */
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private Vector3 testPos;
    private float radius;
    private bool ccActive;
    enum destructibleType
    {
        CRATE,
        BARREL,
        INDESTRUCTIBLE
    }
    [SerializeField] private destructibleType dType;

    // By default, debugging is set to false. If you want to test the debugging features, set debugTesting to true in the Unity Inspector.
    [SerializeField] private bool debugTesting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        cc.enabled = false;

        switch (dType)
        {
            case destructibleType.CRATE: sr.color = Color.white; break;
            case destructibleType.BARREL: sr.color = Color.red; break;
            case destructibleType.INDESTRUCTIBLE: sr.color = Color.grey; break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        testPos = transform.position;
        radius = cc.radius;
        ccActive = cc.enabled;
        if (debugTesting && !Input.GetMouseButton(0)) { rb.gravityScale = 1; }
    }

    // For debugging purposes, pressing [spacebar] while hovering over the destructible will trigger the destroy command.
    // When the destructible is a BARREL, pressing [A] while hovering will toggle the explosion radius view.
    private void OnMouseOver()
    {
       if (debugTesting)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch (dType)
                {
                    case destructibleType.CRATE: break;
                    case destructibleType.BARREL: cc.enabled = true; break;
                    case destructibleType.INDESTRUCTIBLE: break;
                }

                if (dType != destructibleType.INDESTRUCTIBLE)
                {
                    Debug.Log(dType.ToString() + " destroyed.");
                    Object.Destroy(gameObject);
                }
                else { Debug.Log("This is indestructible."); }
            }

            if (Input.GetKeyDown(KeyCode.A) && dType == destructibleType.BARREL)
            {
                if (cc.enabled == true)
                {
                    cc.enabled = false;
                }
                else { cc.enabled = true; }
            }
        }
    }
    // The object can also be dragged with [left-click].
    private void OnMouseDrag()
    {
        if (Input.GetMouseButton(0) && debugTesting)
        {
            rb.gravityScale = 0;
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = 10;
            transform.position = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        }
    }

    // Gizmo to draw the barrel's explosion radius.
    private void OnDrawGizmos()
    {
        if (debugTesting && dType == destructibleType.BARREL && ccActive)
        {
            Gizmos.color = new Color(1, 0, 0, 0.75f);
            Gizmos.DrawSphere(testPos, radius);
        }
    }
}
