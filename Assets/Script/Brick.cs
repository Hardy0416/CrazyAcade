using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public List<GameObject> Items = new List<GameObject>();
    public GameObject explosiveeffect;
    
    private void OnDisable() // �ı��ɶ�
    {
        RandomItemInstantiate();
        Explosiveeffect();
    }
    void Explosiveeffect()
    {
        GameObject gameObject = Instantiate(explosiveeffect, transform.position, transform.rotation);
        Destroy(gameObject, 1f);
    }

    void RandomItemInstantiate()
    {
        int probability = Random.Range(0,4);
        int randomNum = Random.Range(0,3);

        if (probability == 1) {//20% Ȯ���� ������ ����
            Instantiate(Items[randomNum], transform.position, transform.rotation);
        }
    }

    

}
