using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldMap
{
    public class Level : Pin
    {
        public Pin[] levelPaths = new Pin[4];

        [SerializeField] private new string name;
        [TextArea()]
        [SerializeField] private string description;
        [SerializeField] private int sceneBuildIndex;
        private bool isLocked = true;

        public string Name { get { return this.name; } }
        public string Description { get { return this.description; } }

        public void UnLockLevel()
        {
            // Play unlocking sequence?
            isLocked = false;
        }

        public void PlayLevel()
        { 
            // TODO: add a confimation or ready up portion.
            // Call the sceneManager and load the level with {sceneBuildIndex}

        }

    }
}
