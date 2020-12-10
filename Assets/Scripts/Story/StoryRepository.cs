using System.Collections.Generic;
using System.Linq;
using Script;
using UnityEngine;

namespace Story
{
    public class StoryRepository
    {
        SqliteDatabase sqlDB = new SqliteDatabase("config.db");
        public Story[] GetAll()
        {
            var sql = "select * from story";
            var dataTable = sqlDB.ExecuteQuery(sql);
            return dataTable.Rows.Select((r) => new Story((int)r["id"], JsonUtility.FromJson<Sentences>((string)r["sentences"]))).ToArray();
        }
        public Sentence[] Get(int id)
        {
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