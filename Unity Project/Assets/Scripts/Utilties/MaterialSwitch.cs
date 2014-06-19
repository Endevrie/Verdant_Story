using UnityEngine;
using System.Collections;


//This class holds a default material and an array of other materials to switch to
//This class provides an easy way to switch between several materials for instance between a non glow shader and a glow shader.
public class MaterialSwitch : MonoBehaviour 
{
    [SerializeField]
    private Material m_Default;
    [SerializeField]
    private Material [] m_Other;
    [SerializeField]
    private MeshRenderer m_MeshRenderer = null;

    private void OnEnable()
    {
        if (m_MeshRenderer == null)
        {
            m_MeshRenderer = GetComponent<MeshRenderer>();
        }
        if (m_MeshRenderer == null)
        {
            Debug.Log("Please attach a mesh renderer to this game object (" + name + ")");
            enabled = false;
        }
    }

    //Sets the mesh renderer's material back to default
    public void setDefault()
    {
        if (m_MeshRenderer != null)
        {
            m_MeshRenderer.material = m_Default;
        }
    }
    //Sets the mesh renderers material to one of the others
    public void setOther(int aIndex)
    {
        if (m_MeshRenderer != null)
        {
            if (aIndex >= 0 && aIndex < m_Other.Length)
            {
                m_MeshRenderer.material = m_Other[aIndex];
            }
        }
    }

}
