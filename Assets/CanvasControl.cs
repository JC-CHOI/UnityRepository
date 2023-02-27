using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasControl : MonoBehaviour
{
    [SerializeField]
    Image m_image;
    [SerializeField]
    Text m_text;

    [SerializeField]
    Sprite[] m_sprites;

    // Start is called before the first frame update
    void Start()
    {
        m_text.text = "Test Text";
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.A))
        {
            m_text.text = "A";
            m_image.sprite = m_sprites[0];
        }
        else if( Input.GetKeyDown(KeyCode.S))
        {
            m_image.sprite = m_sprites[1];
            m_text.text = "S";
        }
        else if( Input.GetKeyDown(KeyCode.D))
        {
            m_image.sprite = m_sprites[2];
            m_text.text = "D";
        }
    }

    public void ButtonEvent()
    {
        Debug.Log("¹öÆ°");
    }
    public void ButtonDownEvent()
    {
        Debug.Log("Button Down");
        SceneManager.LoadScene(3);
    }
    public void ButtonUpEvent()
    {
        Debug.Log("Button Up");
    }
}
