using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Particles{ 
    public class Particle
    {
        private string name_;

        private GameObject gameObject_;

        public Object object_; 
        
        private bool splitable_;

        public Particle (string name, GameObject gameObject, bool splitable) { 
            name_ = name;
            gameObject_ = gameObject;
            splitable_ = splitable;
        }

        public bool getSplitable() { return splitable_; }

        public GameObject getGameObject() { return gameObject_; }

        public string getName() { return name_; }

        public void setGameObject(GameObject obj) { gameObject_ = obj; }

        public void setObject(Object obj) { object_ = obj; }

    }

    


}

