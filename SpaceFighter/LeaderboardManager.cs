using System;
using System.Collections.Generic;

namespace SpaceFighter
{
    public class LeaderboardManager
    {

        private static Lazy<LeaderboardManager> instance = new Lazy<LeaderboardManager>(() => new LeaderboardManager());
        public static LeaderboardManager Instance { get { return instance.Value; } }

        const string LEADERBOARD_FILE = "leaderboard.txt";

        public void SaveLeaderboard(float time)
        {
            using System.IO.StreamWriter file = new System.IO.StreamWriter(LEADERBOARD_FILE, true);

            file.WriteLine(DateTime.Now + "," + time);
        }

        public List<LeaderboardEntry> LoadLeaderboard()
        {
            List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

            if (!System.IO.File.Exists(LEADERBOARD_FILE))
            {
                return leaderboard;
            }

            using System.IO.StreamReader file = new System.IO.StreamReader(LEADERBOARD_FILE);



            string line;
            while ((line = file.ReadLine()) != null)
            {
                string[] lineParts = line.Split(',');
                LeaderboardEntry entry = new LeaderboardEntry()
                {
                    Date = DateTime.Parse(lineParts[0]),
                    Score = float.Parse(lineParts[1])
                };
                leaderboard.Add(entry);
            }

            return leaderboard;
        }

    }

}
