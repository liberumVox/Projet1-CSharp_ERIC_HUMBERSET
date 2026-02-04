using GestionScolarite.ControlLayer;
using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.ModelLayer;
using GestionScolarite.ViewLayer;
using Moq;

namespace GestionScolarite.Tests
{
    [TestClass]
    public class ProfesseurControlleurTests
    {
        [TestMethod]
        public void AfficherTous_AppelleDAOEtView()
        {
            var mockDAO = new Mock<IProfesseurDAO>();
            var mockView = new Mock<ProfesseurView>();
            
            mockDAO.Setup(d => d.GetAll()).Returns(new List<Professeur>
            {
                new Professeur(1, "Tremblay", "Jean", "Informatique")
            });

            var controlleur = new ProfesseurControlleur(mockDAO.Object, mockView.Object);
            controlleur.AfficherTous();

            mockDAO.Verify(d => d.GetAll(), Times.Once);
            mockView.Verify(v => v.AfficherListe(It.IsAny<List<(int, string, string, string)>>()), Times.Once);
        }

        [TestMethod]
        public void AfficherProfesseur_AvecIdValide_AfficheProfesseur()
        {
            var mockDAO = new Mock<IProfesseurDAO>();
            var mockView = new Mock<ProfesseurView>();
            var prof = new Professeur(1, "Tremblay", "Jean", "Informatique");
            
            mockDAO.Setup(d => d.GetById(1)).Returns(prof);
            mockView.Setup(v => v.DemanderId()).Returns(1);

            var controlleur = new ProfesseurControlleur(mockDAO.Object, mockView.Object);
            controlleur.AfficherProfesseur();

            mockView.Verify(v => v.AfficherProfesseur(1, "Tremblay", "Jean", "Informatique"), Times.Once);
        }

        [TestMethod]
        public void AfficherProfesseur_AvecIdInvalide_AfficheMessage()
        {
            var mockDAO = new Mock<IProfesseurDAO>();
            var mockView = new Mock<ProfesseurView>();
            
            mockDAO.Setup(d => d.GetById(999)).Returns((Professeur?)null);
            mockView.Setup(v => v.DemanderId()).Returns(999);

            var controlleur = new ProfesseurControlleur(mockDAO.Object, mockView.Object);
            controlleur.AfficherProfesseur();

            mockView.Verify(v => v.AfficherMessage("Professeur introuvable."), Times.Once);
        }

        [TestMethod]
        public void SupprimerProfesseur_AppelleDAOSupprimer()
        {
            var mockDAO = new Mock<IProfesseurDAO>();
            var mockView = new Mock<ProfesseurView>();
            
            mockView.Setup(v => v.DemanderId()).Returns(1);

            var controlleur = new ProfesseurControlleur(mockDAO.Object, mockView.Object);
            controlleur.SupprimerProfesseur();

            mockDAO.Verify(d => d.Supprimer(1), Times.Once);
            mockView.Verify(v => v.AfficherMessage("Professeur supprimé."), Times.Once);
        }

        [TestMethod]
        public void RechercherParDepartement_RetourneProfsDuDept()
        {
            var mockDAO = new Mock<IProfesseurDAO>();
            var mockView = new Mock<ProfesseurView>();
            
            mockDAO.Setup(d => d.GetByDepartement("Informatique")).Returns(new List<Professeur>
            {
                new Professeur(1, "Tremblay", "Jean", "Informatique"),
                new Professeur(2, "Gagnon", "Marie", "Informatique")
            });
            mockView.Setup(v => v.DemanderDepartement()).Returns("Informatique");

            var controlleur = new ProfesseurControlleur(mockDAO.Object, mockView.Object);
            controlleur.RechercherParDepartement();

            mockDAO.Verify(d => d.GetByDepartement("Informatique"), Times.Once);
            mockView.Verify(v => v.AfficherListe(It.Is<List<(int, string, string, string)>>(
                l => l.Count == 2)), Times.Once);
        }
    }
}
