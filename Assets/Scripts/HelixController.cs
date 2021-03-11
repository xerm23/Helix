using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector3 startRotation;
    private Vector2 lastTapPos;

    public Transform topTransform; //pocetna platforma
    public Transform goalTransform; //ciljna platforma
    private float helixDistance; //razmak izmedju njih 
    public GameObject helixLevelPrefab; //prefab platforme za stvaranje    
    public GameObject verticalPrefab; //prefab vertical za stvaranje
    public GameObject verticalBigPrefab; //prefab vertical Big za stvaranje
    public int brojplatformi;
    List<GameObject> listaPlatformi = new List<GameObject>();


    void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + .1f);
        LoadStage();
    }
	

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && GameManager.singleton.playable==1){
            Vector2 curTapPos = Input.mousePosition;

            if (lastTapPos == Vector2.zero)
                lastTapPos = curTapPos;

            float delta = lastTapPos.x - curTapPos.x;
            lastTapPos = curTapPos;

            transform.Rotate(Vector3.up * delta/2);
        }
        if (Input.GetMouseButtonUp(0))
        {
            lastTapPos = Vector2.zero;
        }
    }

    private void BrisanjePlaformi()
    {
        for (int i = 0; i < listaPlatformi.Count; i++)
        {
            Destroy(listaPlatformi[i].gameObject);
        }
    }

    public void LoadStage()
    {
        transform.localEulerAngles = startRotation;

        float levelDistance = helixDistance / brojplatformi;
        float spawnPosY = topTransform.localPosition.y;

        BrisanjePlaformi();

        for (int i = 0; i < brojplatformi-1; i++)
        {
            spawnPosY -= levelDistance;
            GameObject platforma = Instantiate(helixLevelPrefab, transform);
            listaPlatformi.Add(platforma);
            platforma.transform.localPosition = new Vector3(0, spawnPosY, 0);            

            //otvori na platformi, mora biti minimum jedan
            int partsToDisable = Random.Range(1, 8);

            for (int j = 0; j < partsToDisable; j++)
            {
                GameObject randomPart = platforma.transform.GetChild(Random.Range(0, platforma.transform.childCount)).gameObject;
                Destroy(randomPart);

            }
            if (partsToDisable > 5) platforma.gameObject.AddComponent<SpiningScript>();

            int bigExist = 0;
            //pravljenje death delova
            foreach (Transform t in platforma.transform)
            {
                    int rand = Random.Range(1, 5); // 20% sanse da bude death part
                    if (rand == 1) t.gameObject.AddComponent<DeathPart>();


                    int rand2 = Random.Range(1, 7); // 1/7 sanse da doda vertical 
                    if (rand2 == 1)
                    {
                        GameObject vert = Instantiate(verticalPrefab, t.gameObject.transform);
                    }

                    int rand3 = Random.Range(1, 15); // 1/15 sanse da doda vertical 
                    if (rand3 == 1 && bigExist == 0)
                    {
                        GameObject vert = Instantiate(verticalBigPrefab, t.gameObject.transform);
                        bigExist = 1;
                    }
                

            }


        }

    }
}
