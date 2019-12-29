using Microsoft.EntityFrameworkCore;
using SwissVoting.DAL.DB;
using SwissVoting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwissVoting.DAL
{
    public class SwissVotingRepo : ISwissVotingRepo
    {
        public Dictionary<int, VoteCount> GetVotesByCanton()
        {
            using var db = new SwissContext();
            using var connection = db.Database.GetDbConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "select * from public.get_votes()";
            connection.Open();
            using var reader = command.ExecuteReader();

            var results = new Dictionary<int, VoteCount>();

            if (reader.HasRows)
                while (reader.Read())
                    results.Add(reader.IsDBNull(0) ? 0 : reader.GetInt32(0), new VoteCount
                    {
                        For = reader.GetInt64(1),
                        Against = reader.GetInt64(2)
                    });

            reader.Close();

            return results;
        }
    }
}
