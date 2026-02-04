using GestionScolarite.ModelLayer;

namespace GestionScolarite.ViewLayer
{
    internal class EtudiantView
    {
        // la vue ne doit pas manipuler directement les objets du modèle métier (Etudiant, Cours, Inscription).
        // Elle doit etre découplé du modèle et seulement interagir via : des valeurs primitives(string, int, decimal…)

        public int AfficherMenuEtudiant()
        {
            Console.WriteLine("\n--- GESTION DES ÉTUDIANTS ---");
            Console.WriteLine("1. Lister les étudiants");
            Console.WriteLine("2. Ajouter un étudiant");
            Console.WriteLine("3. Modifier un étudiant");
            Console.WriteLine("4. Supprimer un étudiant");
            Console.WriteLine("0. Retour au menu principal");
            Console.Write("Choix : ");
            int.TryParse(Console.ReadLine(), out int choix);
            return choix;
        }

        public (string prenom, string nom) SaisirInfosEtudiant()
        {
            Console.Write("Prénom : ");
            string prenom = Console.ReadLine();
            Console.Write("Nom : ");
            string nom = Console.ReadLine();
            return (prenom, nom);
        }

        public int DemanderIdEtudiant()
        {
            Console.Write("ID de l’étudiant : ");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        public void AfficherListe(List<(int id, string prenom, string nom)> etudiants)
        {
            Console.WriteLine("\nListe des étudiants :");
            foreach (var e in etudiants)
            {
                Console.WriteLine($"[{e.id}] {e.prenom} {e.nom}");
            }
        }

        public void AfficherMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}


