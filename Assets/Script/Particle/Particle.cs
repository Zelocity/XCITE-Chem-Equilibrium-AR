using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Particles{ 
    public class Particle
    {
        private string name_;

        private GameObject gameObject_;
        
        private bool splitable_;

        List<Particle> particles;

        public Particle (string name, GameObject gameObject, bool splitable) { 
            name_ = name;
            gameObject_ = gameObject;
            splitable_ = splitable;
        }


        //GETTERS
        public bool getSplitable() { return splitable_; }

        public GameObject getGameObject() { return gameObject_; }

        public string getName() { return name_; }

        public List<Particle> getSplitParticles () { return particles; }

        //SETTERS
        public void setGameObject(GameObject obj) { gameObject_ = obj; }

        public void setName(string name) { name_ = name; }

        public void setSplitable(bool splitable) { splitable_ = splitable; }

        public void setSplitParticles (List<Particle> particleList) { particles = particleList; }

        

    }

    


}

