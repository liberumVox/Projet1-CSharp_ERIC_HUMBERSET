using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionScolarite.ViewLayer
{
    internal class InscriptionView
    {
        public int AfficherMenuInscription()
        {
            Console.WriteLine("\n--- GESTION DES INSCRIPTIONS ---");
            Console.WriteLine("1. Lister les inscriptions d'un étudiant");
            Console.WriteLine("2. Lister les inscriptions d'un cours");
            Console.WriteLine("3. Inscrire un étudiant à un cours");
            Console.WriteLine("4. Désinscrire un étudiant");
            Console.WriteLine("0. Retour au menu principal");
            Console.Write("Choix : ");
            int.TryParse(Console.ReadLine(), out int choix);
            return choix;
        }

        public int DemanderIdEtudiant()
        {
            Console.Write("ID de l'étudiant : ");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        public int DemanderIdCours()
        {
            Console.Write("ID du cours : ");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        public string DemanderSession()
        {
            Console.Write("Session (ex: A2024, H2025) : ");
            return Console.ReadLine();
        }

        public int? DemanderNote()
        {
            Console.Write("Note (laisser vide si pas encore évaluée) : ");
            string saisie = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(saisie))
                return null;
            
            return int.Parse(saisie);
        }

        public (int etudiantId, int coursId, string session, int? note) SaisirInfosInscription()
        {
            int etudiantId = DemanderIdEtudiant();
            int coursId = DemanderIdCours();
            string session = DemanderSession();
            int? note = DemanderNote();
            
            return (etudiantId, coursId, session, note);
        }

        public void AfficherInscriptionsEtudiant(List<(string prenom, string nom, string codeCours, string titreCours, string session, int? note)> inscriptions)
        {
            Console.WriteLine("\nInscriptions de l'étudiant :");
            foreach (var i in inscriptions)
            {
                string noteAffichee = i.note.HasValue ? i.note.Value.ToString() : "N/A";
                Console.WriteLine($"  {i.codeCours} - {i.titreCours} | Session: {i.session} | Note: {noteAffichee}");
            }
        }

        public void AfficherInscriptionsCours(List<(string prenom, string nom, string session, int? note)> inscriptions)
        {
            Console.WriteLine("\nÉtudiants inscrits au cours :");
            foreach (var i in inscriptions)
            {
                string noteAffichee = i.note.HasValue ? i.note.Value.ToString() : "N/A";
                Console.WriteLine($"  {i.prenom} {i.nom} | Session: {i.session} | Note: {noteAffichee}");
            }
        }

        public void AfficherMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
