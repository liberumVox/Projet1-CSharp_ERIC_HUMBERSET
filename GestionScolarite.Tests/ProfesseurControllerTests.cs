using Microsoft.VisualStudio.TestTools.UnitTesting;
using GestionScolarite.ControlLayer;
using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.ModelLayer;
using Moq;
using System.Collections.Generic;

namespace GestionScolarite.Tests
{
    [TestClass]
    public class ProfesseurControllerTests
    {
        // Test 1 : Obtenir tous les professeurs
        [TestMethod]
        public void TestObtenirTousProfesseurs()
        {
            // Arrange - Préparer les données fictives
            // Créer une liste de profs fictifs
            List<Professeur> profsFictifs = new List<Professeur>();
            profsFictifs.Add(new Professeur(1, "Dupont", "Jean", "Informatique"));
            profsFictifs.Add(new Professeur(2, "Martin", "Marie", "Mathématiques"));
            profsFictifs.Add(new Professeur(3, "Bernard", "Luc", "Physique"));

            // Créer le mock du DAO
            Mock<IProfesseurDAO> mockDao = new Mock<IProfesseurDAO>();
            
            // Configurer le mock pour retourner ma liste fictive
            mockDao.Setup(d => d.GetAll()).Returns(profsFictifs);

            // Créer le contrôleur avec le mock
            ProfesseurControlleur controller = new ProfesseurControlleur(mockDao.Object);

            // Act - Exécuter la méthode à tester
            List<Professeur> resultat = controller.ObtenirTousProfesseurs();

            // Assert - Vérifier les résultats
            Assert.IsNotNull(resultat);
            Assert.AreEqual(3, resultat.Count);
            Assert.AreEqual("Dupont", resultat[0].Nom);
        }

        // Test 2 : Obtenir un professeur par id
        [TestMethod]
        public void TestObtenirProfesseur()
        {
            // Arrange
            Professeur profFictif = new Professeur(1, "Dupont", "Jean", "Informatique");

            Mock<IProfesseurDAO> mockDao = new Mock<IProfesseurDAO>();
            mockDao.Setup(d => d.GetById(1)).Returns(profFictif);

            ProfesseurControlleur controller = new ProfesseurControlleur(mockDao.Object);

            // Act
            Professeur resultat = controller.ObtenirProfesseur(1);

            // Assert
            Assert.IsNotNull(resultat);
            Assert.AreEqual(1, resultat.Id);
            Assert.AreEqual("Dupont", resultat.Nom);
        }

        // Test 3 : Professeur inexistant retourne null
        [TestMethod]
        public void TestObtenirProfesseurInexistant()
        {
            // Arrange
            Mock<IProfesseurDAO> mockDao = new Mock<IProfesseurDAO>();
            mockDao.Setup(d => d.GetById(999)).Returns((Professeur?)null);

            ProfesseurControlleur controller = new ProfesseurControlleur(mockDao.Object);

            // Act
            Professeur resultat = controller.ObtenirProfesseur(999);

            // Assert
            Assert.IsNull(resultat);
        }
    }
}
