using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionScolarite.ViewLayer
{
    internal class CoursView
    {
        // La vue ne manipule que des types primitifs, pas des objets du domaine

        public int AfficherMenuCours()
        {
            Console.WriteLine("\n--- GESTION DES COURS ---");
            Console.WriteLine("1. Lister les cours");
            Console.WriteLine("2. Ajouter un cours");
            Console.WriteLine("3. Modifier un cours");
            Console.WriteLine("4. Supprimer un cours");
            Console.WriteLine("5. Rechercher par code");
            Console.WriteLine("0. Retour au menu principal");
            Console.Write("Choix : ");
            int.TryParse(Console.ReadLine(), out int choix);
            return choix;
        }

        public (string titre, string code) SaisirInfosCours()
        {
            Console.Write("Titre : ");
            string titre = Console.ReadLine();
            Console.Write("Code : ");
            string code = Console.ReadLine();
            return (titre, code);
        }

        public int DemanderIdCours()
        {
            Console.Write("ID du cours : ");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        public string DemanderCodeCours()
        {
            Console.Write("Code du cours : ");
            return Console.ReadLine();
        }

        public void AfficherListe(List<(int id, string titre, string code)> cours)
        {
            Console.WriteLine("\nListe des cours :");
            foreach (var c in cours)
            {
                Console.WriteLine($"[{c.id}] {c.code} - {c.titre}");
            }
        }

        public void AfficherCours(int id, string titre, string code)
        {
            Console.WriteLine($"\nCours trouvé :");
            Console.WriteLine($"[{id}] {code} - {titre}");
        }

        public void AfficherMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
