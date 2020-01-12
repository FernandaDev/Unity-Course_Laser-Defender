using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.5f;

    Material m_Material;
    Vector2 offset;

    private void Start()
    {
        m_Material = GetComponent<Renderer>().material;
        offset = new Vector2(0,backgroundScrollSpeed);
    }

    private void Update()
    {
        m_Material.mainTextureOffset += offset * Time.deltaTime;
    }

}
