using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionDirect : MonoBehaviour, IMovePosition
{
    private IMoveVelocity move;
    private Vector3 movePosition;
    private Animator anim2;
    private Transform transform2;

    private void Awake()
    {
        move = GetComponent<IMoveVelocity>();
        movePosition = transform.position;
        anim2 = transform.Find("Ogre").gameObject.GetComponent<Animator>();
        transform2 = transform.Find("Ogre").gameObject.transform;
    }
    public void SetMovePosition(Vector3 position)
    {
        this.movePosition = position;
    }

    // Update is called once per frame
    private void Update()
    {
    
    Vector2 a = movePosition, b = transform.position;
     if (Vector2.Distance(a, b) > 1f)
        {
            Vector3 moveDir = (movePosition - transform.position).normalized;
            moveDir.z = 0;
            GetComponent<IMoveVelocity>().SetVelocity(moveDir);
            move.SetVelocity(moveDir);
            anim2.SetBool("walking", true);

            Vector3 charscale = transform2.localScale;
            
            

            if(moveDir.x < 0){
                charscale.x = -Mathf.Abs(charscale.x);
            }else{
                charscale.x = Mathf.Abs(charscale.x);
            }
            transform2.localScale = charscale;
        }
        else
        {
            move.SetVelocity(Vector3.zero);
            anim2.SetBool("walking", false);
        }

    }


}
