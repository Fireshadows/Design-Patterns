using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class GameObjectPool : IPool<GameObject>
    {
        private bool m_isDisposed;

        private readonly uint m_expandBy;
        private readonly GameObject m_prefab;
        private readonly Transform m_parent;

        readonly Stack<GameObject> m_objects = new Stack<GameObject>();
        readonly List<GameObject> m_created = new List<GameObject>();

        public GameObjectPool(uint p_initSize, GameObject p_prefab, uint p_expandBy = 1, Transform p_parent = null)
        {
            m_expandBy = (uint)Mathf.Max(1, m_expandBy);

            m_prefab = p_prefab;
            m_parent = p_parent;
            m_prefab.SetActive(false);
            Expand((uint)Mathf.Max(1, p_initSize));
        }

        private void Expand(uint p_amount)
        {
            for (int i = 0; i < p_amount; i++)
            {
                GameObject m_instance = Object.Instantiate(m_prefab, m_parent);
                EmitOnDisable emitOnDisable = m_instance.AddComponent<EmitOnDisable>();
                emitOnDisable.OnDisableGameObject += UnRent;

                m_objects.Push(m_instance);
                m_created.Add(m_instance);
            }
        }

        public GameObject Rent(bool p_returnActive)
        {
            if (m_isDisposed)
                return null;

            if (m_objects.Count == 0)
            {
                Expand(m_expandBy);
            }
            GameObject m_instance = m_objects.Pop();
            m_instance.SetActive(p_returnActive);

            return m_instance;
        }

        public void UnRent(GameObject p_object)
        {
            if (!m_isDisposed)
            {
                m_objects.Push(p_object);
            }
        }

        public void Dispose()
        {
            m_isDisposed = true;
            Clear();
        }

        void Clear()
        {
            foreach (GameObject m_gameObject in m_created)
            {
                if (!m_gameObject)
                {
                    m_gameObject.GetComponent<EmitOnDisable>().OnDisableGameObject -= UnRent;
                    Object.Destroy(m_gameObject);
                }
            }
            m_objects.Clear();
            m_created.Clear();
        }
    }
}