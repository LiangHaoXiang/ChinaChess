using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour {

    private bool canMove;

    public Vector3[] grids;

    public GameObject Ju;

    void Awake()
    {
        grids = new Vector3[90];
        for(int i = 0; i < 90; i++)
        {
            grids[i] = GameObject.Find("Grids").transform.GetChild(i).position;
        }
    }

    void Start ()
    {

        canMove = false;
    }
	
	void Update ()
    {
        if (canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                for(int i = 0; i < grids.Length; i++)
                {
                    if (grids[i].x - 30 <= Input.mousePosition.x && Input.mousePosition.x <= grids[i].x + 30 &&
                        grids[i].y - 27.5 <= Input.mousePosition.y && Input.mousePosition.y <= grids[i].y + 27.5) 
                    {
                        Debug.Log(Input.mousePosition);
                        Ju.transform.position = grids[i];
                    }
                }
                canMove = false;
                Ju.transform.FindChild("白边").gameObject.SetActive(false);
            }
        }
	}

    public void OnClick()
    {
        canMove = true;
        Ju.transform.FindChild("白边").gameObject.SetActive(true);
    }
}
