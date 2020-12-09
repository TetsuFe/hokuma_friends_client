using System;

namespace Story
{
    [Serializable]
    public class Sentence
    {
        public Sentence(string characterName, string body, string characterImagePath, string characterImageEffect)
        {
            this.characterName = characterName;
            this.body = body;
            this.characterImagePath = characterImagePath;
            this.characterImageEffect = characterImageEffect;
        }
        
        public string characterName;
        public string body;
        public string characterImagePath;
        public string characterImageEffect;
    }
}