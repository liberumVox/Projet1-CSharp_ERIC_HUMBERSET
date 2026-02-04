using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.ModelLayer;
using GestionScolarite.ViewLayer;

namespace GestionScolarite.ControlLayer
{
    public class ProfesseurControlleur
    {
        private IProfesseurDAO dao;
        private ProfesseurView view;

        public ProfesseurControlleur(IProfesseurDAO dao, ProfesseurView view)
        {
            this.dao = dao;
            this.view = view;
        }

        public void AjouterProfesseur()
        {
            var (nom, prenom, departement) = view.DemanderInfos();
            
            if (string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(prenom))
            {
                view.AfficherMessage("Le nom et le prénom sont obligatoires.");
                return;
            }

            var prof = new Professeur(nom, prenom, departement);
            dao.Ajouter(prof);
            view.AfficherMessage("Professeur ajouté avec succès.");
        }

        public void ModifierProfesseur()
        {
            int id = view.DemanderId();
            var prof = dao.GetById(id);
            
            if (prof == null)
            {
                view.AfficherMessage("Professeur introuvable.");
                return;
            }

            var (nom, prenom, departement) = view.DemanderInfos();
            prof.Nom = nom;
            prof.Prenom = prenom;
            prof.Departement = departement;
            
            dao.Modifier(prof);
            view.AfficherMessage("Professeur modifié avec succès.");
        }

        public void SupprimerProfesseur()
        {
            int id = view.DemanderId();
            dao.Supprimer(id);
            view.AfficherMessage("Professeur supprimé.");
        }

        public void AfficherProfesseur()
        {
            int id = view.DemanderId();
            var prof = dao.GetById(id);
            
            if (prof == null)
            {
                view.AfficherMessage("Professeur introuvable.");
                return;
            }

            view.AfficherProfesseur(prof.Id, prof.Nom, prof.Prenom, prof.Departement);
        }

        public void AfficherTous()
        {
            var profs = dao.GetAll();
            var liste = profs.Select(p => (p.Id, p.Nom, p.Prenom, p.Departement)).ToList();
            view.AfficherListe(liste);
        }

        public void RechercherParDepartement()
        {
            var dept = view.DemanderDepartement();
            var profs = dao.GetByDepartement(dept);
            var liste = profs.Select(p => (p.Id, p.Nom, p.Prenom, p.Departement)).ToList();
            view.AfficherListe(liste);
        }

        // === MÉTHODES POUR LES TESTS UNITAIRES (SANS VIEW) ===
        
        // Constructeur simplifié pour les tests (injection de dépendance)
        public ProfesseurControlleur(IProfesseurDAO dao)
        {
            this.dao = dao;
        }

        // Méthode pour obtenir tous les professeurs
        public List<Professeur> ObtenirTousProfesseurs()
        {
            // Appeler le DAO pour récupérer tous les profs
            List<Professeur> profs = dao.GetAll();
            return profs;
        }

        // Méthode pour obtenir un professeur par son id
        public Professeur ObtenirProfesseur(int id)
        {
            // Appeler le DAO pour récupérer le prof
            Professeur prof = dao.GetById(id);
            return prof;
        }
    }
}
