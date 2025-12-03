using UnityEngine;

public class backgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.02f;
    Material material;
    Vector2 offset;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollSpeed);    
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
