namespace Advant
{
    using UnityEngine;
    using Newtonsoft.Json;
    public class SaveController :SaveProvider<Save>
    {
        const string _saveKeyName = "Save";     
        /// <summary>
        /// Saves settings as string in PlayerPrefs
        /// </summary>
        /// <param name="save"> Object for storing game progression </param>
        public override void Save(Save save)
        {
            string serializedData = JsonConvert.SerializeObject(save);
            PlayerPrefs.SetString("Save", serializedData);
        }
        /// <summary>
        /// Loads the settings as a string in PlayerPrefs
        /// </summary>
        /// <returns> Returns SaveData </returns>
        public override bool TryLoad(out Save save)
        {
            string saveStr = PlayerPrefs.GetString("Save");
            Debug.Log($"SAVE: {saveStr}");
            if (string.IsNullOrEmpty(saveStr))
            {
                save = new Save(0);
                return false;
            }
            try
            {
                save = JsonConvert.DeserializeObject<Save>(saveStr);
                return true;
            }
            catch
            {
                PlayerPrefs.DeleteKey("Save");
                Debug.LogError("CORRUPTED SAVE FILE RETURNING EMPTY");
                save = new Save(0);
                return false;
            }
        }
        /// <summary>
        /// Returns true if PlayerPrefs has a save key
        /// </summary>
        /// <returns></returns>
        public override bool HasSave()
        {
            return PlayerPrefs.HasKey(_saveKeyName);
        }
#if UNITY_EDITOR
        /// <summary>
        /// Delete existing save in PlayerPrefs
        /// </summary>
        public void DeleteSave() 
        {
            PlayerPrefs.DeleteKey(_saveKeyName);
        }
#endif
    }
}
