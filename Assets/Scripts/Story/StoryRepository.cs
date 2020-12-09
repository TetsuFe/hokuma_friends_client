using System.Collections.Generic;
using Script;
using UnityEngine;

namespace Story
{
    public class StoryRepository
    {
        public Sentence[] Get(int id)
        {
            SqliteDatabase sqlDB = new SqliteDatabase("config.db");
            var sql = "select * from story where id="+id;
            DataTable dataTable = sqlDB.ExecuteQuery(sql);
            foreach (var row in dataTable.Rows)
            {
                var story = new Story((int) row["id"], JsonUtility.FromJson<Sentences>((string)row["sentences"]));
                return story.sentences.sentences;
            }
            return null;
        }
    }
}