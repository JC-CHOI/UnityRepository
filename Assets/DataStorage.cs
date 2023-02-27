using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    [SerializeField]
    int m_nIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_nIndex = FindObjectsOfType<DataStorage>().Length - 1;
        Debug.Log(m_nIndex.ToString() + "object created");

        if (FindObjectsOfType<DataStorage>().Length > 1)
        {
            Debug.Log("Destroy object index : " + m_nIndex.ToString());
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
