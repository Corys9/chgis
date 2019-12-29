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
        public Dictionary<int, VoteCount> GetVotesByCanton(int lawID)
        {
            using var db = new SwissContext();
            using var connection = db.Database.GetDbConnection();
            using var command = connection.CreateCommand();
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
            command.CommandText = $"select * from public.get_votes({lawID})";
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
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

        public List<Law> GetLaws()
        {
            using var db = new SwissContext();

            var laws = from law in db.Laws
                       orderby law.CreatedOn descending
                       select new Law
                       {
                           ID = law.Id,
                           Owner = law.Owner,
                           Proposal = law.Proposal,
                           FiledOn = law.CreatedOn
                       };

            return laws.ToList();
        }
    }
}
