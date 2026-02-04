using GestionScolarite.ModelLayer;

namespace GestionScolarite.ViewLayer
{
    public class ProfesseurView
    {
        public virtual void AfficherMenu()
        {
            Console.WriteLine("\n=== Gestion des Professeurs ===");
            Console.WriteLine("1. Ajouter un professeur");
            Console.WriteLine("2. Modifier un professeur");
            Console.WriteLine("3. Supprimer un professeur");
            Console.WriteLine("4. Afficher un professeur");
            Console.WriteLine("5. Afficher tous les professeurs");
            Console.WriteLine("6. Rechercher par département");
            Console.WriteLine("0. Retour");
        }

        public virtual (string nom, string prenom, string departement) DemanderInfos()
        {
            Console.Write("Nom : ");
            var nom = Console.ReadLine() ?? "";
            Console.Write("Prénom : ");
            var prenom = Console.ReadLine() ?? "";
            Console.Write("Département : ");
            var departement = Console.ReadLine() ?? "";
            return (nom, prenom, departement);
        }

        public virtual int DemanderId()
        {
            Console.Write("ID du professeur : ");
            return int.Parse(Console.ReadLine() ?? "0");
        }

        public virtual string DemanderDepartement()
        {
            Console.Write("Département : ");
            return Console.ReadLine() ?? "";
        }

        public virtual void AfficherProfesseur(int id, string nom, string prenom, string departement)
        {
            Console.WriteLine($"[{id}] {prenom} {nom} - {departement}");
        }

        public virtual void AfficherListe(List<(int id, string nom, string prenom, string departement)> professeurs)
        {
            if (professeurs.Count == 0)
            {
                Console.WriteLine("Aucun professeur trouvé.");
                return;
            }

            foreach (var prof in professeurs)
            {
                Console.WriteLine($"[{prof.id}] {prof.prenom} {prof.nom} - {prof.departement}");
            }
        }

        public virtual void AfficherMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
