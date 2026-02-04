using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.ViewLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionScolarite.ControlLayer
{
    internal class InscriptionControlleur
    {
        private readonly IInscriptionDAO inscriptionDAO;
        private readonly InscriptionView view;
        private readonly IDao<ModelLayer.Etudiant> etudiantDAO;
        private readonly IDao<ModelLayer.Cours> coursDAO;

        public InscriptionControlleur(
            IInscriptionDAO inscriptionDAO, 
            InscriptionView view,
            IDao<ModelLayer.Etudiant> etudiantDAO,
            IDao<ModelLayer.Cours> coursDAO)
        {
            this.inscriptionDAO = inscriptionDAO;
            this.view = view;
            this.etudiantDAO = etudiantDAO;
            this.coursDAO = coursDAO;
        }

        public void GererMenuInscription()
        {
            bool continuer = true;

            while (continuer)
            {
                int choix = view.AfficherMenuInscription();

                switch (choix)
                {
                    case 1:
                        ListerInscriptionsEtudiant();
                        break;
                    case 2:
                        ListerInscriptionsCours();
                        break;
                    case 3:
                        InscrireEtudiant();
                        break;
                    case 4:
                        DesinscrireEtudiant();
                        break;
                    case 0:
                        continuer = false;
                        break;
                    default:
                        view.AfficherMessage("Choix invalide.");
                        break;
                }
            }
        }

        private void ListerInscriptionsEtudiant()
        {
            int etudiantId = view.DemanderIdEtudiant();

            // Vérifier que l'étudiant existe
            var etudiant = etudiantDAO.GetById(etudiantId);
            if (etudiant == null)
            {
                view.AfficherMessage("Étudiant introuvable.");
                return;
            }

            List<ModelLayer.Inscription> inscriptions = inscriptionDAO.GetInscriptionsParEtudiant(etudiantId);

            if (inscriptions.Count == 0)
            {
                view.AfficherMessage("Aucune inscription pour cet étudiant.");
                return;
            }

            // Transformer en tuples pour la vue
            List<(string prenom, string nom, string codeCours, string titreCours, string session, int? note)> liste 
                = new List<(string, string, string, string, string, int?)>();

            foreach (var insc in inscriptions)
            {
                liste.Add((
                    insc.Etudiant.Prenom,
                    insc.Etudiant.Nom,
                    insc.Cours.Code,
                    insc.Cours.Titre,
                    insc.Session,
                    insc.Note
                ));
            }

            view.AfficherInscriptionsEtudiant(liste);
        }

        private void ListerInscriptionsCours()
        {
            int coursId = view.DemanderIdCours();

            // Vérifier que le cours existe
            var cours = coursDAO.GetById(coursId);
            if (cours == null)
            {
                view.AfficherMessage("Cours introuvable.");
                return;
            }

            List<ModelLayer.Inscription> inscriptions = inscriptionDAO.GetInscriptionsParCours(coursId);

            if (inscriptions.Count == 0)
            {
                view.AfficherMessage("Aucune inscription pour ce cours.");
                return;
            }

            // Transformer en tuples pour la vue
            List<(string prenom, string nom, string session, int? note)> liste 
                = new List<(string, string, string, int?)>();

            foreach (var insc in inscriptions)
            {
                liste.Add((
                    insc.Etudiant.Prenom,
                    insc.Etudiant.Nom,
                    insc.Session,
                    insc.Note
                ));
            }

            view.AfficherInscriptionsCours(liste);
        }

        private void InscrireEtudiant()
        {
            (int etudiantId, int coursId, string session, int? note) = view.SaisirInfosInscription();

            // Valider que l'étudiant existe
            var etudiant = etudiantDAO.GetById(etudiantId);
            if (etudiant == null)
            {
                view.AfficherMessage("Étudiant introuvable.");
                return;
            }

            // Valider que le cours existe
            var cours = coursDAO.GetById(coursId);
            if (cours == null)
            {
                view.AfficherMessage("Cours introuvable.");
                return;
            }

            // Valider la session
            if (string.IsNullOrWhiteSpace(session))
            {
                view.AfficherMessage("Session invalide.");
                return;
            }

            // Créer l'inscription
            var inscription = new ModelLayer.Inscription(etudiant, cours, session, note);

            try
            {
                inscriptionDAO.Ajouter(inscription);
                view.AfficherMessage("Inscription réussie.");
            }
            catch (Exception ex)
            {
                view.AfficherMessage($"Erreur lors de l'inscription : {ex.Message}");
            }
        }

        private void DesinscrireEtudiant()
        {
            int etudiantId = view.DemanderIdEtudiant();
            int coursId = view.DemanderIdCours();
            string session = view.DemanderSession();

            try
            {
                inscriptionDAO.Supprimer(etudiantId, coursId, session);
                view.AfficherMessage("Désinscription réussie.");
            }
            catch (Exception ex)
            {
                view.AfficherMessage($"Erreur lors de la désinscription : {ex.Message}");
            }
        }
    }
}
