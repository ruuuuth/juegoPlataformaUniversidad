using UnityEngine;

public class AutoScroller : MonoBehaviour
{
    public float scrollerSpeed = 0.05f;

    [SerializeField] private MeshRenderer mesh;

    void Update()
    {
        Vector2 offset = new Vector2(Time.time * scrollerSpeed, 0);
        mesh.material.mainTextureOffset = offset;
    }
}