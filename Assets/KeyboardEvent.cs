using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardEvent : MonoBehaviour
{
    [SerializeField]
    GameObject m_menu;


    // Start is called before the first frame update
    void Start()
    {
        if( m_menu == null)
        {
            m_menu = GetComponentInChildren<Canvas>().gameObject;
        }
        m_menu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Escape))
        {
            m_menu.SetActive(!m_menu.activeSelf);
        }
    }
}
