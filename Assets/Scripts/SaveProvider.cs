namespace Advant
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    [Serializable]
    public abstract class SaveProvider<T> : MonoBehaviour where T : struct
    {
        /// <summary>
        /// Save data via save provider
        /// </summary>
        /// <param name="t"></param>
        public abstract void Save(T t);
        /// <summary>
        /// Return true when save is valid and load save
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public abstract bool TryLoad(out T t);
        /// <summary>
        /// Return true when save provider has valid save.otherwise return false
        /// </summary>
        /// <returns></returns>
        public abstract bool HasSave();
    }
    public class SaveProviderEvent<T> : UnityEvent<T, bool>
    {
    }
}
