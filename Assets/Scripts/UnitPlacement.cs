using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitPlacement : MonoBehaviour
{
    private int m_ArrayNum = 0;
    private bool m_P2Turn = false;
    
    [SerializeField] private Text m_Player1Text;
    [SerializeField] private Text m_Player2Text;
    [SerializeField] private Text m_EndText;
    
    // Start is called before the first frame update
    void Start()
    {
        m_ArrayNum = 0;
        Camera.main.gameObject.transform.position = new Vector3(0, 50, 0);
        Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0);

        m_Player1Text.GetComponent<Outline>().enabled = true;
        m_Player2Text.GetComponent<Outline>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Ground")
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
                go.transform.position = hit.point + (Vector3.up * 6);
                m_ArrayNum++;
                
                GameManager.instance.m_as.clip = GameManager.instance.m_UnitPlace;
                GameManager.instance.m_as.Play();

                if (m_P2Turn == false)
                {
                    if (m_ArrayNum < GameManager.instance.m_P1Units.Count)
                    {
                        int unitsLeft = GameManager.instance.m_P1Units.Count - m_ArrayNum;
                        m_Player1Text.text = "Red Team:\n" + unitsLeft;
                    }
                    else if (m_ArrayNum >= GameManager.instance.m_P1Units.Count)
                    {
                        m_ArrayNum = 0;
                        m_Player1Text.text = "Red Team:\n" + GameManager.instance.m_P1Units.Count;
                        m_Player2Text.text = "Blue Team:\n" + GameManager.instance.m_P2Units.Count;
                        
                        m_Player1Text.GetComponent<Outline>().enabled = false;
                        m_Player2Text.GetComponent<Outline>().enabled = true;
                        m_P2Turn = true;
                    }
                }
                else if (m_P2Turn == true)
                {
                    if (m_ArrayNum < GameManager.instance.m_P2Units.Count)
                    {
                        int unitsLeft = GameManager.instance.m_P2Units.Count - m_ArrayNum;
                        m_Player2Text.text = "Blue Team:\n" + unitsLeft;
                    }
                    else if(m_ArrayNum >= GameManager.instance.m_P2Units.Count)
                    {
                        m_ArrayNum = 0;
                        m_Player1Text.text = "Red Team:\n" + GameManager.instance.m_P1Units.Count;
                        m_Player2Text.text = "Blue Team:\n" + GameManager.instance.m_P2Units.Count;
                        
                        m_Player1Text.GetComponent<Outline>().enabled = false;
                        m_Player2Text.GetComponent<Outline>().enabled = false;
                        m_EndText.gameObject.SetActive(true);
                        this.enabled = false;
                    }
                }
            }
        }
    }
}
