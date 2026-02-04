using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.ModelLayer;
using Moq;

namespace GestionScolarite.Tests
{
    [TestClass]
    public class ProfesseurDAOTests
    {
        [TestMethod]
        public void GetAll_RetournePlusieursProfs()
        {
            var mockDAO = new Mock<IProfesseurDAO>();
            mockDAO.Setup(d => d.GetAll()).Returns(new List<Professeur>
            {
                new Professeur(1, "Tremblay", "Jean", "Informatique"),
                new Professeur(2, "Gagnon", "Marie", "Mathématiques")
            });

            var result = mockDAO.Object.GetAll();

            Assert.HasCount(2, result);
            Assert.AreEqual("Tremblay", result[0].Nom);
        }

        [TestMethod]
        public void GetById_RetourneProfValide()
        {
            var mockDAO = new Mock<IProfesseurDAO>();
            var profAttendu = new Professeur(1, "Tremblay", "Jean", "Informatique");
            mockDAO.Setup(d => d.GetById(1)).Returns(profAttendu);

            var result = mockDAO.Object.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Tremblay", result.Nom);
            Assert.AreEqual("Jean", result.Prenom);
        }

        [TestMethod]
        public void GetById_RetourneNullSiInexistant()
        {
            var mockDAO = new Mock<IProfesseurDAO>();
            mockDAO.Setup(d => d.GetById(999)).Returns((Professeur?)null);

            var result = mockDAO.Object.GetById(999);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Ajouter_AppeleLaMethode()
        {
            var mockDAO = new Mock<IProfesseurDAO>();
            var nouveauProf = new Professeur("Dupont", "Pierre", "Physique");

            mockDAO.Object.Ajouter(nouveauProf);

            mockDAO.Verify(d => d.Ajouter(It.IsAny<Professeur>()), Times.Once);
        }

        [TestMethod]
        public void Modifier_AppeleLaMethode()
        {
            var mockDAO = new Mock<IProfesseurDAO>();
            var prof = new Professeur(1, "Tremblay", "Jean", "Informatique");

            mockDAO.Object.Modifier(prof);

            mockDAO.Verify(d => d.Modifier(It.IsAny<Professeur>()), Times.Once);
        }

        [TestMethod]
        public void Supprimer_AppeleLaMethode()
        {
            var mockDAO = new Mock<IProfesseurDAO>();

            mockDAO.Object.Supprimer(1);

            mockDAO.Verify(d => d.Supprimer(1), Times.Once);
        }

        [TestMethod]
        public void GetByDepartement_RetourneProfsDuDepartement()
        {
            var mockDAO = new Mock<IProfesseurDAO>();
            mockDAO.Setup(d => d.GetByDepartement("Informatique")).Returns(new List<Professeur>
            {
                new Professeur(1, "Tremblay", "Jean", "Informatique"),
                new Professeur(3, "Lavoie", "Sylvie", "Informatique")
            });

            var result = mockDAO.Object.GetByDepartement("Informatique");

            Assert.HasCount(2, result);
            Assert.IsTrue(result.All(p => p.Departement == "Informatique"));
        }

        [TestMethod]
        public void GetByNom_RetourneProfAvecNom()
        {
            var mockDAO = new Mock<IProfesseurDAO>();
            var prof = new Professeur(1, "Tremblay", "Jean", "Informatique");
            mockDAO.Setup(d => d.GetByNom("Tremblay")).Returns(prof);

            var result = mockDAO.Object.GetByNom("Tremblay");

            Assert.IsNotNull(result);
            Assert.AreEqual("Tremblay", result.Nom);
        }
    }
}
