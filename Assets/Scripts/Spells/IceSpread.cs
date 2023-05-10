using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpread : MonoBehaviour, ISpell
{
    public List<GameObject> spellParts;
    public float damage { get; set; }
    public float dmgMultiplier { get; set; }
    public int dmgLayer { get; set; }

    //TEMP
    public Transform cameraHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            RaycastHit closestHit;
            Vector3 origin = transform.position + new Vector3(0, 0.1f, 3);
            Vector3 target;
            if (Physics.Raycast(origin, cameraHolder.forward, out closestHit, Mathf.Infinity))
            {
                target = closestHit.point;
            }
            else
            {
                target = transform.position + cameraHolder.forward * 1000;
            }
            Cast(origin, target);
        }
    }



    public void Cast(Vector3 origin, Vector3 target)
    {
        int numberOfIcicles = Random.Range(3, 8);
        for (int i = 0; i < numberOfIcicles; i++)
        {
            MakeIcicle((origin+new Vector3(Mathf.Sin((float)i / numberOfIcicles * 2 * Mathf.PI) * 0.5f, Mathf.Cos((float)i / numberOfIcicles * 2 * Mathf.PI) * 0.5f)),target);
        }
    }

    private void MakeIcicle(Vector3 origin, Vector3 target)
    {
        GameObject icicle = Instantiate(spellParts[0], origin, cameraHolder.rotation);
        icicle.GetComponent<IceSpread>().cameraHolder = cameraHolder;
    }
}
