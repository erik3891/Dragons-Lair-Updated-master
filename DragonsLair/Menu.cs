using System;
using System.IO;

namespace DragonsLair
{
    public class Menu
    {
        private Controller control = new Controller();
        
        public void Show()
        {
            bool running = true;
            do
            {
                ShowMenu();
                string choice = GetUserChoice();
                switch (choice)
                {
                    case "0":
                        running = false;
                        break;
                    case "1":
                        ShowScore();
                        break;
                    case "2":
                        ScheduleNewRound();
                        break;
                    case "3":
                        SaveMatch();
                        break;
                    case "4":
                        Creatacount();
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        Console.ReadLine();
                        break;
                }
            } while (running);
        }

        private void ShowMenu()
        {
            Console.WriteLine("Dragons Lair");
            Console.WriteLine();
            Console.WriteLine("1. Præsenter turneringsstilling");
            Console.WriteLine("2. Planlæg runde i turnering");
            Console.WriteLine("3. Registrér afviklet kamp");
            Console.WriteLine("4. Opret Konto");
            Console.WriteLine("");
            Console.WriteLine("0. Exit");
        }

        private string GetUserChoice()
        {
            Console.WriteLine();
            Console.Write("Indtast dit valg: ");
            return Console.ReadLine();
        }
        
        private void ShowScore()
        {
            Console.Write("Angiv navn på turnering: ");
            string tournamentName = Console.ReadLine();
            Console.Clear();
            control.ShowScore(tournamentName);
        }

        private void ScheduleNewRound()
        {
            Console.Write("Angiv navn på turnering: ");
            string tournamentName = Console.ReadLine();
            Console.Clear();
            control.ScheduleNewRound(tournamentName);
        }

        private void SaveMatch()
        {
            Console.Write("Angiv navn på turnering: ");
            string tournamentName = Console.ReadLine();
            Console.Write("Angiv runde: ");
            int round = int.Parse(Console.ReadLine());
            Console.Write("Angiv vinderhold: ");
            string winner = Console.ReadLine();
            Console.Clear();
            //control.SaveMatch(tournamentName, round, winner);
        }
        //private void SaveMatch()
        //{
        //    Console.Write("Angiv navn på turnering: ");
        //    string tournamentName = Console.ReadLine();
        //    Console.Write("Angiv runde: ");
        //    int round = int.Parse(Console.ReadLine());
        //    Console.Write("Angiv vinderhold: ");
        //    string winner = Console.ReadLine();
        //    Console.Clear();
        //    control.SaveMatch(tournamentName, round, winner);
       // }
        private static void Creatacount()
        {
            StreamWriter writer = new StreamWriter(@"../../CreateAccount.txt", true);
            Console.Write("Angiv Konto Navn: ");
            string accountName = Console.ReadLine();
        
            Console.Write("Angiv Password: ");
            string passwordName = Console.ReadLine();

            Console.Write("Addresse: ");
            string Addresse = (Console.ReadLine());

            Console.Write("PhoneNumber: ");
            string PhoneNumber = Console.ReadLine();

            Console.Write("DOB: ");
            string DOB = Console.ReadLine();

            Console.Write("Email: ");
            string Email = Console.ReadLine();

            writer.WriteLine($"{accountName};{passwordName};{Addresse};{PhoneNumber};{DOB};{Email}");
            writer.Close();

            Console.WriteLine("Bekræftelse mail sent til " + Email);
            Console.WriteLine("Tryk 'Enter' igen for at afslutte");
            Console.ReadLine();
            Console.Clear();
        }

}
}