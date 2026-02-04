using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.ModelLayer;
using GestionScolarite.ViewLayer;

namespace GestionScolarite.ControlLayer
{
    internal class CoursControlleur
    {
        private readonly ICoursDAO coursDAO;
        private readonly CoursView view;

        public CoursControlleur(ICoursDAO coursDAO, CoursView view)
        {
            this.coursDAO = coursDAO;
            this.view = view;
        }

        public void GererMenuCours()
        {
            bool continuer = true;

            while (continuer)
            {
                int choix = view.AfficherMenuCours();

                switch (choix)
                {
                    case 1:
                        ListerCours();
                        break;
                    case 2:
                        AjouterCours();
                        break;
                    case 3:
                        ModifierCours();
                        break;
                    case 4:
                        SupprimerCours();
                        break;
                    case 5:
                        RechercherParCode();
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

        private void ListerCours()
        {
            List<Cours> cours = coursDAO.GetAll();

            List<(int id, string titre, string code)> liste = new List<(int, string, string)>();

            foreach (var c in cours)
            {
                liste.Add((c.Id, c.Titre, c.Code));
            }

            view.AfficherListe(liste);
        }

        private void AjouterCours()
        {
            (string titre, string code) = view.SaisirInfosCours();

            if (string.IsNullOrWhiteSpace(titre) || string.IsNullOrWhiteSpace(code))
            {
                view.AfficherMessage("Titre et code requis.");
                return;
            }

            Cours nouveauCours = new Cours(titre, code);
            coursDAO.Ajouter(nouveauCours);

            view.AfficherMessage("Cours ajouté.");
        }

        private void ModifierCours()
        {
            int id = view.DemanderIdCours();
            Cours cours = coursDAO.GetById(id);

            if (cours == null)
            {
                view.AfficherMessage("Cours introuvable.");
                return;
            }

            (string titre, string code) = view.SaisirInfosCours();

            if (string.IsNullOrWhiteSpace(titre) || string.IsNullOrWhiteSpace(code))
            {
                view.AfficherMessage("Champs invalides.");
                return;
            }

            cours.Titre = titre;
            cours.Code = code;
            coursDAO.Modifier(cours);
            view.AfficherMessage("Cours modifié.");
        }

        private void SupprimerCours()
        {
            int id = view.DemanderIdCours();
            Cours cours = coursDAO.GetById(id);

            if (cours == null)
            {
                view.AfficherMessage("Cours introuvable.");
                return;
            }

            coursDAO.Supprimer(id);
            view.AfficherMessage("Cours supprimé.");
        }

        private void RechercherParCode()
        {
            string code = view.DemanderCodeCours();
            Cours cours = coursDAO.GetByCode(code);

            if (cours == null)
            {
                view.AfficherMessage("Aucun cours trouvé avec ce code.");
                return;
            }

            view.AfficherCours(cours.Id, cours.Titre, cours.Code);
        }
    }
}
