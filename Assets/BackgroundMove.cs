using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    //배경 이동 속도
    [SerializeField]
    float fMoveSpeed = 1f;

    //배경이 이동하고 난 후 리셋될 초기 위치
    [SerializeField]
    Vector2 vStartPosition;

    //배경이 이동하여 초기위치로 갈 마지막 위치
    [SerializeField]
    Vector2 vEndPosition;

    //생성할 때 x값의 위치 간격 변수
    [SerializeField]
    float fPositionOffeset = 23f;

    PlayerMove player;

    //배경이미지 오브젝트 배열
    GameObject[] backgrounds;
    //배경이미지 오브젝트 배열 인덱스
    public int nBackgroundIndex = 0;

    //플레이어이의 이전 프레임 위치를 저장
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
                //생성된 오브젝트의 부모를 자기 자신의 부모로 설정
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
                //오른쪽 middle생성
                GameObject createdObj = Instantiate(gameObject);
                createdObj.transform.parent = transform.parent;
                createdObj.transform.position =
                    new Vector3(
                        transform.position.x + fPositionOffeset,
                        transform.position.y,
                        transform.position.z
                    );
                //생성한 오브젝트 인덱스 설정
                createdObj.GetComponent<BackgroundMove>().nBackgroundIndex = 1;
                createdObj.GetComponent<BackgroundMove>().followTarget = gameObject;

                //왼쪽 middle생성
                createdObj = Instantiate(gameObject);
                createdObj.transform.parent = transform.parent;
                createdObj.transform.position =
                    new Vector3(
                        transform.position.x - fPositionOffeset,
                        transform.position.y,
                        transform.position.z
                    );
                //생성한 오브젝트 인덱스 설정
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
            //Tag가 Background_sky인지 판단 후 실행
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
            //절대값 얻어오는 함수 Mathf.Abs
            float fDistance = Mathf.Abs(vBackupPlayerPosision.x - player.gameObject.transform.position.x);
            if (fDistance > 0.01f)
            {
                //Tag가 Background_middle인지 판단 후 실행
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
                        //0번 ----
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
                        //0번 ----
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
            //Tag가 그 외의 것인지 판단 후 실행
            Debug.LogWarning("BasckgorundMove.cs : Tag가 잘못되었습니다.");
        }
    }
}