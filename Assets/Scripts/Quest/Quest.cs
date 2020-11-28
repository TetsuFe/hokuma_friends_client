using System;

namespace Quest
{
    [Serializable]
    public class Quest
    {
        public Quest(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public int id;
        public string name;
    }
}