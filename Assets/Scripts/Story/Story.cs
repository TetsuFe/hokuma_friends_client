using System;

namespace Story
{
    [Serializable]
    public class Story
    {
        public int id;
        public string title;
        public Sentences sentences;

        public Story(int id, string title, Sentences sentences)
        {
            this.id = id;
            this.sentences = sentences;
            this.title = title;
        }
        
    }

    [Serializable]
    public class Sentences
    {
        public Sentence[] sentences;
    }
}