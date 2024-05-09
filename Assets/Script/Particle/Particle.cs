using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Particles{ 
    public class Particle
    {
        private string name_;

        private GameObject gameObject_;
        
        private bool splitable_;

        public Particle (string name, GameObject gameObject, bool splitable) { 
            name_ = name;
            gameObject_ = gameObject;
            splitable_ = splitable;
        }


        //GETTERS
        public bool getSplitable() { return splitable_; }

        public GameObject getGameObject() { return gameObject_; }

        public string getName() { return name_; }

        //SETTERS
        public void setGameObject(GameObject obj) { gameObject_ = obj; }

    }

    


}

