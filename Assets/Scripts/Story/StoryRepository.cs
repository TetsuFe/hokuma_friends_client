using System;
using System.Collections.Generic;
using System.Linq;
using DataVersion;
using MasterDataVersion;
using Script;
using UnityEngine;

namespace Story
{
    [Serializable]
    public class Story2
    {
        public int id;
        public string title;
        public string sentences;
        public string created_at;
        public string updated_at;
    }

    public class StoryRepository
    {
        SqliteDatabase sqlDB = new SqliteDatabase("config.db");

        public Story[] GetAll()
        {
            var sql = "select * from story";
            var dataTable = sqlDB.ExecuteQuery(sql);
            return dataTable.Rows.Select((r) => new Story((int) r["id"], (string) r["title"],
                JsonUtility.FromJson<Sentences>((string) r["sentences"]))).ToArray();
        }

        public Sentence[] Get(int id)
        {
            var sql = "select * from story where id=" + id;
            DataTable dataTable = sqlDB.ExecuteQuery(sql);
            foreach (var row in dataTable.Rows)
            {
                var story = new Story((int) row["id"], (string) row["title"],
                    JsonUtility.FromJson<Sentences>((string) row["sentences"]));
                return story.sentences.sentences;
            }

            return null;
        }

        public async void UpdateFromMasterData()
        {
            var localDataVersion = new LocalDataVersionRepository().GetLocalDataVersion();
            var masterDataVersion = await new MasterDataVersionApi().GetStoryMasterDataVersion();
            var stories = await new StoryApi().GetAll();
            foreach (var story in stories)
            {
                if (localDataVersion < masterDataVersion)
                {
                    Debug.Log(story.id);
                    Debug.Log(story.title);
                    var sql = $"insert into story Values({story.id}, '{story.title}', '{story.sentences}')";
                    sqlDB.ExecuteNonQuery(sql);
                }
            }
        }
    }
}