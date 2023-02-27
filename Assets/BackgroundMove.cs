using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    //��� �̵� �ӵ�
    [SerializeField]
    float fMoveSpeed = 1f;

    //����� �̵��ϰ� �� �� ���µ� �ʱ� ��ġ
    [SerializeField]
    Vector2 vStartPosition;

    //����� �̵��Ͽ� �ʱ���ġ�� �� ������ ��ġ
    [SerializeField]
    Vector2 vEndPosition;

    //������ �� x���� ��ġ ���� ����
    [SerializeField]
    float fPositionOffeset = 23f;

    PlayerMove player;

    //����̹��� ������Ʈ �迭
    GameObject[] backgrounds;
    //����̹��� ������Ʈ �迭 �ε���
    public int nBackgroundIndex = 0;

    //�÷��̾����� ���� ������ ��ġ�� ����
    Vector3 vBackupPlayerPosision = Vector3.zero;

    public GameObject followTarget;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();

        if (gameObject.CompareTag("Background_sky"))
        {
            backgrounds = GameObject.FindGameObjectsWithTag(gameObject.tag);
            nBackgroundIndex = backgrounds.Length - 1;

            if (nBackgroundIndex < 2)
            {
                GameObject createdObj = Instantiate(gameObject, new Vector3(transform.position.x + fPositionOffeset, transform.position.y, transform.position.z), Quaternion.identity);
                //������ ������Ʈ�� �θ� �ڱ� �ڽ��� �θ�� ����
                createdObj.transform.parent = transform.parent;
            }
        }
        else if (gameObject.CompareTag("Background_middle"))
        {
            backgrounds = GameObject.FindGameObjectsWithTag(gameObject.tag);
            vBackupPlayerPosision = player.gameObject.transform.position;
            //nBackgroundIndex = backgrounds.Length - 1;

            if (nBackgroundIndex == 0)
            {
                //������ middle����
                GameObject createdObj = Instantiate(gameObject);
                createdObj.transform.parent = transform.parent;
                createdObj.transform.position =
                    new Vector3(
                        transform.position.x + fPositionOffeset,
                        transform.position.y,
                        transform.position.z
                    );
                //������ ������Ʈ �ε��� ����
                createdObj.GetComponent<BackgroundMove>().nBackgroundIndex = 1;
                createdObj.GetComponent<BackgroundMove>().followTarget = gameObject;

                //���� middle����
                createdObj = Instantiate(gameObject);
                createdObj.transform.parent = transform.parent;
                createdObj.transform.position =
                    new Vector3(
                        transform.position.x - fPositionOffeset,
                        transform.position.y,
                        transform.position.z
                    );
                //������ ������Ʈ �ε��� ����
                createdObj.GetComponent<BackgroundMove>().nBackgroundIndex = 2;
                createdObj.GetComponent<BackgroundMove>().followTarget = gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        backgrounds = GameObject.FindGameObjectsWithTag(gameObject.tag);
        if (gameObject.CompareTag("Background_sky"))
        {
            //backgrounds = GameObject.FindGameObjectsWithTag(gameObject.tag);
            //Tag�� Background_sky���� �Ǵ� �� ����
            transform.Translate(Vector2.left * Time.deltaTime * fMoveSpeed, Space.Self);
            if (transform.localPosition.x <= -fPositionOffeset)
            {
                int nFrontBackgroundIndex = (nBackgroundIndex + 2) % 3;
                transform.localPosition =
                    new Vector3(
                        backgrounds[nFrontBackgroundIndex].transform.localPosition.x + fPositionOffeset,
                        transform.localPosition.y,
                        transform.localPosition.z
                        );
            }
        }
        else if (gameObject.CompareTag("Background_middle"))
        {
            //���밪 ������ �Լ� Mathf.Abs
            float fDistance = Mathf.Abs(vBackupPlayerPosision.x - player.gameObject.transform.position.x);
            if (fDistance > 0.01f)
            {
                //Tag�� Background_middle���� �Ǵ� �� ����
                if (nBackgroundIndex == 0)
                {
                    transform.Translate(-player.vMoveDir * Time.deltaTime * player.fMoveSpeed * 10f, Space.Self);
                }
                else
                {
                    if (transform.localPosition.x > followTarget.transform.localPosition.x)
                    {
                        transform.localPosition = new Vector3(followTarget.transform.localPosition.x + fPositionOffeset,
                                followTarget.transform.localPosition.y,
                                followTarget.transform.localPosition.z);
                    }
                    else if (transform.localPosition.x < followTarget.transform.localPosition.x)
                    {
                        transform.localPosition = new Vector3(followTarget.transform.localPosition.x - fPositionOffeset,
                                followTarget.transform.localPosition.y,
                                followTarget.transform.localPosition.z);
                    }
                }

                if (transform.localPosition.x < -fPositionOffeset && player.vMoveDir.x > 0)
                {
                    int nFrontBackgroundIndex = (nBackgroundIndex + 2) % 3;
                    //transform.localPosition = new Vector3(backgrounds[nFrontBackgroundIndex].transform.localPosition.x + fPositionOffeset,transform.localPosition.y,transform.localPosition.z);

                    if (nBackgroundIndex == 1)
                    {
                        followTarget = backgrounds[0];
                        transform.localPosition = new Vector3(backgrounds[0].transform.localPosition.x + fPositionOffeset, transform.localPosition.y, transform.localPosition.z);
                    }
                    else if (nBackgroundIndex == 2)
                    {
                        followTarget = backgrounds[1];
                        transform.localPosition = new Vector3(backgrounds[1].transform.localPosition.x + fPositionOffeset, transform.localPosition.y, transform.localPosition.z);
                    }
                    else
                    {
                        //0�� ----
                        transform.localPosition = new Vector3(backgrounds[nFrontBackgroundIndex].transform.localPosition.x + fPositionOffeset, transform.localPosition.y, transform.localPosition.z);
                        backgrounds[1].GetComponent<BackgroundMove>().followTarget = backgrounds[2];
                        backgrounds[2].GetComponent<BackgroundMove>().followTarget = backgrounds[0];
                    }
                }
                else if (transform.localPosition.x > fPositionOffeset && player.vMoveDir.x < 0)
                {
                    int nFrontBackgroundIndex = (nBackgroundIndex + 1) % 3;
                    transform.localPosition = new Vector3(backgrounds[nFrontBackgroundIndex].transform.localPosition.x - fPositionOffeset, transform.localPosition.y, transform.localPosition.z);
                    if (nBackgroundIndex == 1)
                    {
                        followTarget = backgrounds[2];
                        transform.localPosition = new Vector3(backgrounds[2].transform.localPosition.x - fPositionOffeset, transform.localPosition.y, transform.localPosition.z);
                    }
                    else if (nBackgroundIndex == 2)
                    {
                        followTarget = backgrounds[0];
                        transform.localPosition = new Vector3(backgrounds[0].transform.localPosition.x - fPositionOffeset, transform.localPosition.y, transform.localPosition.z);
                    }
                    else
                    {
                        //0�� ----
                        transform.localPosition = new Vector3(backgrounds[nFrontBackgroundIndex].transform.localPosition.x + fPositionOffeset, transform.localPosition.y, transform.localPosition.z);
                        backgrounds[1].GetComponent<BackgroundMove>().followTarget = backgrounds[0];
                        backgrounds[2].GetComponent<BackgroundMove>().followTarget = backgrounds[1];
                    }
                }
                vBackupPlayerPosision = player.gameObject.transform.position;
            }
            else if (nBackgroundIndex != 0)
            {
                if (transform.localPosition.x > followTarget.transform.localPosition.x)
                {
                    transform.localPosition = new Vector3(followTarget.transform.localPosition.x + fPositionOffeset,
                            followTarget.transform.localPosition.y,
                            followTarget.transform.localPosition.z);
                }
                else if (transform.localPosition.x < followTarget.transform.localPosition.x)
                {
                    transform.localPosition = new Vector3(followTarget.transform.localPosition.x - fPositionOffeset,
                            followTarget.transform.localPosition.y,
                            followTarget.transform.localPosition.z);
                }
            }
        }
        else
        {
            //Tag�� �� ���� ������ �Ǵ� �� ����
            Debug.LogWarning("BasckgorundMove.cs : Tag�� �߸��Ǿ����ϴ�.");
        }
    }
}