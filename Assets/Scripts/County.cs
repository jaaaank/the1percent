using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class County : MonoBehaviour
{
    public float hapiness;

    public float wealth;
    public float food;
    public float meds;

    // Start is called before the first frame update
    void Start()
    {
        updateHapiness();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateHapiness()
    { 
        hapiness = (wealth+food+meds)/3;
        Vector3 color = Vector3.Lerp(new Vector3(1, 0, 0), new Vector3(0, 1, 0), hapiness / 100);
        gameObject.GetComponent<SpriteRenderer>().color = new Vector4(color.x, color.y, color.z, 1);
    }
}
