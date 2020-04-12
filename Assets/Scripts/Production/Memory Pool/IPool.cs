

namespace Tools
{
    public interface IPool<T>
    {
        T Rent(bool returnActive);
    }
    /*
    public class ComponentPool<T> : IPool<T> where T : Component
    {
        private bool m_isDisposed;

        private readonly uint m_expandBy;
        private readonly Stack<T> m_objects;
        private readonly List<T> m_created;
        private readonly T m_prefab;
        private readonly Transform m_parent;

        public ComponentPool(uint m_initSize, T m_prefab, uint m_expandBy = 1, Transform m_parent = null)
        {
            m_expandBy = m_expandBy;
            m_prefab = m_prefab;
            m_parent = m_parent;

            m_objects = new Stack<T>();
            m_created = new List<T>();

            Expand((uint)Mathf.Max(1, m_initSize));
        }

        private void Expand(uint p_expandBy)
        {
            for (int i = 0; i < p_expandBy; i++)
            {
                T m_instance = Object.Instantiate<T>(m_prefab, m_parent);
                //m_instance.gameObject.AddComponent<EmitOnDisable>();
                m_objects.Push(m_instance);
                m_created.Add(m_instance);
            }
        }

        public T Rent(bool returnActive)
        {
            if (m_objects.Count == 0)
            {
                Expand(m_expandBy);
            }

            T m_instance = m_objects.Pop();
            return m_instance;
        }

        private void UnRent(GameObject p_gameObject)
        {
            if (!m_isDisposed)
            {
                m_objects.Push(p_gameObject.GetComponent<T>());
            }
        }

        public void Clean()
        {
            foreach (T  m_component in m_created)
            {
                if (m_component != null)
                {
                    //m_component.GetComponent<EmitOnDisable>() -= UnRent;
                    Object.Destroy(m_component.gameObject);
                }
            }
            m_created.Clear();
            m_objects.Clear();
        }

        public void Dispose()
        {
            m_isDisposed = true;
            Clean();
        }
    }
    */

}