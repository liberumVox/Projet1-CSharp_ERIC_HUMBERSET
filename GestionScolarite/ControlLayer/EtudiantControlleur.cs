using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.ModelLayer;
using GestionScolarite.ViewLayer;

namespace GestionScolarite.ControlLayer
{
    internal class EtudiantControlleur
        {
            private readonly IEtudiantDAO etudiantDAO;
            private readonly EtudiantView view;

            public EtudiantControlleur(IEtudiantDAO etudiantDAO, EtudiantView view)
            {
                this.etudiantDAO = etudiantDAO;
                this.view = view;
            }

            public void GererMenuEtudiant()
            {
                bool continuer = true;

                while (continuer)
                {
                    int choix = view.AfficherMenuEtudiant();

                    switch (choix)
                    {
                        case 1:
                            ListerEtudiants();
                            break;
                        case 2:
                            AjouterEtudiant();
                            break;
                        case 3:
                            ModifierEtudiant();
                            break;
                        case 4:
                            SupprimerEtudiant();
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

            private void ListerEtudiants()
            {
                List<Etudiant> etudiants = etudiantDAO.GetAll();

                List<(int id, string prenom, string nom)> liste = new List<(int, string, string)>();

                // la boucle qui suit sert à transformer la liste d'objets étudiants en liste de tuples (de variables simples)
                //afin de la transmettre à la vue qui ne manipule pas des objets du domaine directement.
                
                foreach (var etu in etudiants)
                {
                    liste.Add((etu.Id, etu.Prenom, etu.Nom));
                }

                view.AfficherListe(liste);
            }

            private void AjouterEtudiant()
            {
                (string prenom, string nom) = view.SaisirInfosEtudiant();

                if (string.IsNullOrWhiteSpace(prenom) || string.IsNullOrWhiteSpace(nom))
                {
                    view.AfficherMessage("Prénom et nom requis.");
                    return;
                }

                Etudiant nouvelEtudiant = new Etudiant (prenom, nom );
                etudiantDAO.Ajouter(nouvelEtudiant);

                view.AfficherMessage("Étudiant ajouté.");
            }

            private void ModifierEtudiant()
            {
                int id = view.DemanderIdEtudiant();
                Etudiant etudiant = etudiantDAO.GetById(id);

                if (etudiant == null)
                {
                    view.AfficherMessage("Étudiant introuvable.");
                    return;
                }

                (string prenom, string nom) = view.SaisirInfosEtudiant();

                if (string.IsNullOrWhiteSpace(prenom) || string.IsNullOrWhiteSpace(nom))
                {
                    view.AfficherMessage("Champs invalides.");
                    return;
                }

                etudiant.Prenom = prenom;
                etudiant.Nom = nom;
                etudiantDAO.Modifier(etudiant);
                view.AfficherMessage("Étudiant modifié.");
            }

            private void SupprimerEtudiant()
            {
                int id = view.DemanderIdEtudiant();
                Etudiant etudiant = etudiantDAO.GetById(id);

                if (etudiant == null)
                {
                    view.AfficherMessage("Étudiant introuvable.");
                    return;
                }

                etudiantDAO.Supprimer(id);
                view.AfficherMessage("Étudiant supprimé.");
            }
        }
    }

