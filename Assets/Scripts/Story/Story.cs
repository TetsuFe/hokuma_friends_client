using System;

namespace Story
{
    [Serializable]
    public class Story
    {
        public int id;
        public Sentences sentences;

        public Story(int id, Sentences sentences)
        {
            this.id = id;
            this.sentences = sentences;
        }
        
    }

    [Serializable]
    public class Sentences
    {
        public Sentence[] sentences;
    }
}