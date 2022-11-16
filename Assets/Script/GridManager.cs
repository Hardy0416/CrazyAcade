using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //화면에 딱 맞아 떨어지는 그리드 생성을 위한 스크립트
    public GameObject _gameObject;
    public Sprite sprite;
    int vertical,horizontal;
    int width;
    int height;
    
    void Start()
    {
        vertical = (int)Camera.main.orthographicSize;
        horizontal = vertical * (Screen.width / Screen.height); // vertical*2 = horizontal 가로길이가 세로의 길이의 비율을 수식하게됨
        width = horizontal *2;
        height = vertical *2;
        Debug.Log(height);
        Debug.Log(width);

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                Debug.Log("for loop is work");
                SpawnTile(i, j);  //걍 시발 맵을 미리 만들어야 겠다 ㅇㅇ;
                //만들어 놓고 못쓰게된 스크립트 ㅠ
            }
        }
    }
    void SpawnTile(int x, int y)
    {
        GameObject gameObject = new GameObject("x: "+ x +" y:" + y); //이친구를 내가 만든 게임오브젝트를 생성하도록 해서 그 친구에게 collision을 넣고 어찌저찌하면 될듯?
        //Debug.Log(gameObject.transform.position);
        gameObject.transform.position = new Vector2(x - (width - 1f)/2, y - (height - 1f)/2); // 픽셀 값만 큼 width과 height에서 빼준다 픽셀값인 0.5를 빼주어야 하지만 2로 나누기때문에 1을 빼준다
        //Instantiate(gameObject,gridPosition,transform.rotation);
        var s = gameObject.AddComponent<SpriteRenderer>();
        s.sprite = sprite;
        //s.color = new Color(0,1,1);

    }

    
}
