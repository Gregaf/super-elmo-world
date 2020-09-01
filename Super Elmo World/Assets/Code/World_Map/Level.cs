using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WorldMap
{
    public class Level : Pin
    {
        public Pin[] levelPaths = new Pin[4];

        [SerializeField] private string levelName;
        [TextArea()]
        [SerializeField] private string description;
        [SerializeField] private SceneIndexes levelToLoad;
        private bool isLocked = true;
        


        public string Name { get { return this.levelName; } }
        public string Description { get { return this.description; } }
        public SceneIndexes LevelToLoad { get { return this.levelToLoad; } }

        public static event Action<SceneIndexes> OnLevelChosen;

        public void UnLockLevel()
        {
            // Play unlocking sequence?
            isLocked = false;
        }

        public void PlayLevel()
        {
            OnLevelChosen?.Invoke(this.levelToLoad);

        }

    }
}
