using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       //UI 컴포넌트 사용

public class TimeStamp : MonoBehaviour
{
    [SerializeField]
    Text m_timeText;

    [SerializeField]
    float m_fMS;          //밀리세컨드
    [SerializeField]
    int m_nS;           //초
    [SerializeField]
    int m_nM;           //분

    // Start is called before the first frame update
    void Start()
    {
        if (m_timeText == null)
        {
            m_timeText = GetComponent<Text>();
        }
        //시간 초기화
        m_fMS = 0.0f;
        m_nS = 0;
        m_nM = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //한 프레임동안 걸린 시간 ex) 30fps -> 1/30
        m_fMS += Time.deltaTime;
        if (m_fMS >= 1)
        {
            m_nS += 1;
            m_fMS -= 1.0f;
            if (m_nS >= 60)
            {
                m_nS -= 60;
                m_nM += 1;
            }
        }

        if (m_timeText != null)
        {
            string strMS;
            string strS;
            string strM;

            if (m_fMS >= 0.10f)
            {
                strMS = ((int)(m_fMS * 100.0f)).ToString();
            }
            else
            {
                strMS = "0" + ((int)(m_fMS * 100.0f)).ToString();
            }

            if (m_nS >= 10)
            {
                strS = m_nS.ToString();
            }
            else
            {
                strS = "0" + m_nS.ToString();
            }

            if (m_nM >= 10)
            {
                strM = m_nM.ToString();
            }
            else
            {
                strM = "0" + m_nM.ToString();
            }

            m_timeText.text = string.Format("{0}:{1}:{2}", strM, strS, strMS);
        }
    }
}