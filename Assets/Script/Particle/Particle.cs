using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Particles{ 
    public class Particle: MonoBehaviour
    {
        private string name_;

        private GameObject gameObject_;
        
        private bool splitable_;

        public Particle (string name, GameObject gameObject, bool splitable) { 
            name_ = name;
            gameObject_ = gameObject;
            splitable_ = splitable;
        }

        public string get_name() { return name_; }

        public bool get_splitable() { return splitable_; }

        public GameObject get_gameObject() { return gameObject_; }

    }


}

