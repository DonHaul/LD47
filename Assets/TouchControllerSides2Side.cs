using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControllerSides2Side : MonoBehaviour
{

    public float halfscreenWidth=10;
    public float fakeaxis = 0;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Screen.width);
        halfscreenWidth = Screen.width / 2;

#if UNITY_EDITOR
        halfscreenWidth = 720f / 2;
#endif
    
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                // you touched at least one UI element
                return;
            }
        }

        if (Input.touchCount>0)
        {
            Debug.Log(Input.GetTouch(0).position);

            //capture th latest touch
            if(Input.GetTouch(Input.touchCount-1).position.x > halfscreenWidth)
            {
                //right
                fakeaxis = 1;
            }
            else
            {
                //left
                fakeaxis = -1;
            }
                
        } else
        {
            fakeaxis = 0;
        }
        
    }

    public void Jump()
    {
        Player.instance.Jump();
    }
}
