using System.Collections.Generic;
using Script;
using UnityEngine;


namespace Quest
{
    public class QuestRepository
    {
        public List<Quest> GetAll()
        {
            SqliteDatabase sqlDB = new SqliteDatabase("config.db");
            var sql = "select * from quest";
            DataTable dataTable = sqlDB.ExecuteQuery(sql);
            var quests = new List<Quest>();
            foreach (var row in dataTable.Rows)
            {
                quests.Add(new Quest((int) row["id"], (string) row["name"]));
            }

            return quests;
        }
    }
}