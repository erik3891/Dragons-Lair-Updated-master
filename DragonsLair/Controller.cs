using System;
using System.Collections.Generic;
using System.Linq;
using TournamentLib;
using System.Reflection;
using System.Text;

namespace DragonsLair
{
    public class Controller
    {
        private TournamentRepo tournamentRepository = new TournamentRepo();

        public TournamentRepo GetTournamentRepository()
        {
            return tournamentRepository;
        }

        public void ShowScore(string tournamentName)
        {
            Tournament tournament = tournamentRepository.GetTournament(tournamentName);
            List<int> points = new int[tournament.GetTeams().Count].ToList<int>();
            List<Team> teams = tournament.GetTeams();
            List<string> sortedList = new List<string>();

            int rounds = tournament.GetNumberOfRounds();
            for (int i = 0; i < rounds; i++)
            {
                List<Team> winners = tournament.GetRound(i).GetWinningTeams();
                foreach (Team winner in winners)
                {
                    for (int j = 0; j < tournament.GetTeams().Count; j++)
                    {
                        if (winner.Name == tournament.GetTeams()[j].Name)
                        {
                            points[j] = points[j] + 1;
                        }
                    }
                }
            }

            while (points.Count > 0)
            {
                int index = points.IndexOf(points.Max());
                sortedList.Add(teams[index].ToString() + ": " + points[index]);
                points.RemoveAt(index);
                teams.RemoveAt(index);
            }

            sortedList.ForEach(Console.WriteLine);
        }

        public void ScheduleNewRound(string tournamentName, bool printNewMatches = true)
        {
            Tournament tournament = tournamentRepository.GetTournament(tournamentName);
            tournament.SetupTestTeams(); // Setup 8 teams
            Round newRound = new Round();
            Match newMatch;
            
            List<Team> tempTeams = new List<Team>(tournament.GetTeams());
            List<Team> newTeams = new List<Team>();

            int numberOfRound = tournament.GetNumberOfRounds();
            Round lastRound = null; 
            Random rnd = new Random();
            bool isRoundFinished = true;
            Team freeRider = null;

            if (numberOfRound != 0)
            {
                lastRound = tournament.GetRound(numberOfRound);
                isRoundFinished = tournament.GetRound(numberOfRound).IsMatchesFinished();
            }

            if(isRoundFinished)
            {

                if (lastRound != null)
                {
                    tempTeams = tournament.GetRound(numberOfRound).GetWinningTeams();
                    tempTeams.Add(tournament.GetRound(numberOfRound).FreeRider);
                }
                
                while(tempTeams.Count >= 1)
                {
                    if(tempTeams.Count == 1)
                    {
                        freeRider = tempTeams[0];
                        tempTeams.RemoveAt(0);
                    } 
                    else
                    {
                        newMatch = new Match();

                        int rndNumber1 = rnd.Next(tempTeams.Count);
                        Team team1 = tempTeams[rndNumber1];
                        tempTeams.RemoveAt(rndNumber1);

                        int rndNumber2 = rnd.Next(tempTeams.Count);
                        Team team2 = tempTeams[rndNumber2];
                        tempTeams.RemoveAt(rndNumber2);

                        newMatch.FirstOpponent = team1;
                        newMatch.SecondOpponent = team2;
                        newTeams.Add(team1);
                        newTeams.Add(team2);
                        newRound.AddMatch(newMatch);
                    }
                }
                tournament.AddRound(newRound);
                tournament.GetRound(numberOfRound).SetFreeRider(freeRider);
            }

            if(printNewMatches == true)
            { 
                Console.WriteLine("0-------------------------------------------0");
                printLine("Turnering: " + tournamentName);
                printLine("Runde: " + numberOfRound + 1);
                printLine(newTeams.Count / 2 + " kampe");
                Console.WriteLine("0-------------------------------------------0");
                for (int i = 0; i < newTeams.Count; i++)
                {
                    printLine(paddedText(newTeams[i].Name, 20) + " - " + paddedText(newTeams[i + 1].Name, 20));
                    i++;
                }
                Console.WriteLine("0-------------------------------------------0");
                Console.ReadLine();
            }
        }

        //public void SaveMatch(string tournamentName, int round, string team1, string team2, string winner)
        //{
        //    team2 = 'team';


        //}
   
        public string paddedText(string text, int length)
        {
            int runs = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length - text.Length / 2; i++)
            {
                sb.Append(" ");
                runs++;
            }

            if (length > (runs * 2 + text.Length))
            {
                return sb + " " + text + sb;
            } 
            else
            {
                return sb + text + sb;
            }
        }

        public void printLine(string text)
        {
            Console.WriteLine("|" + paddedText(text, 43) + "|");
        }
    }
}
