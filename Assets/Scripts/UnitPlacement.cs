using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacement : MonoBehaviour
{
    private int m_ArrayNum = 0;
    private bool m_P2Turn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        m_ArrayNum = 0;
        Camera.main.gameObject.transform.position = new Vector3(0, 50, 0);
        Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject go;
                if (m_P2Turn == false)
                {
                    go = GameManager.instance.m_P1Units[m_ArrayNum];
                }
                else
                {
                    go = GameManager.instance.m_P2Units[m_ArrayNum];
                }
                Instantiate(go, hit.point, Quaternion.identity);
                m_ArrayNum++;

                if (m_P2Turn == false && m_ArrayNum >= GameManager.instance.m_P1Units.Count)
                {
                    m_ArrayNum = 0;
                    m_P2Turn = true;
                    Debug.Log("Player 2's Turn!");
                }
                else if (m_P2Turn == true && m_ArrayNum >= GameManager.instance.m_P2Units.Count)
                {
                    this.enabled = false;
                }
            }
        }
    }
}
