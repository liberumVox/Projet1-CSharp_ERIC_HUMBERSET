using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GestionScolarite.ModelLayer;
using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.ControlLayer;
using System.Collections.Generic;

namespace GestionScolarite.Tests
{
    [TestClass]
    public class MatiereFictiveControlleurTests
    {
        // Test 1 : Obtenir toutes les matières fictives
        [TestMethod]
        public void TestObtenirToutesMatieres_RetourneListe()
        {
            // Arrange - Créer les données fictives
            List<MatiereFictive> matieresFictives = new List<MatiereFictive>();
            matieresFictives.Add(new MatiereFictive(1, "420-137-GG", "Programmation Objet II", 90));
            matieresFictives.Add(new MatiereFictive(2, "420-138-GG", "Programmation Web", 90));
            matieresFictives.Add(new MatiereFictive(3, "201-NYA-05", "Calcul différentiel", 75));

            // Créer le mock du DAO inexistant
            Mock<IMatiereFictiveDAO> mockDao = new Mock<IMatiereFictiveDAO>();
            
            // Configurer le mock pour retourner la liste fictive
            mockDao.Setup(d => d.GetAll()).Returns(matieresFictives);

            // Créer le controlleur avec le mock
            MatiereFictiveControlleur controlleur = new MatiereFictiveControlleur(mockDao.Object);

            // Act - Exécuter la méthode à tester
            List<MatiereFictive> resultat = controlleur.ObtenirToutesMatieres();

            // Assert - Vérifier les résultats
            Assert.IsNotNull(resultat);
            Assert.AreEqual(3, resultat.Count);
            Assert.AreEqual("420-137-GG", resultat[0].Code);
            Assert.AreEqual("Programmation Objet II", resultat[0].Nom);
        }

        // Test 2 : Obtenir une matière par id
        [TestMethod]
        public void TestObtenirMatiere_ParId_RetourneMatiere()
        {
            // Arrange
            MatiereFictive matiereFictive = new MatiereFictive(1, "420-137-GG", "Programmation Objet II", 90);

            Mock<IMatiereFictiveDAO> mockDao = new Mock<IMatiereFictiveDAO>();
            mockDao.Setup(d => d.GetById(1)).Returns(matiereFictive);

            MatiereFictiveControlleur controlleur = new MatiereFictiveControlleur(mockDao.Object);

            // Act
            MatiereFictive resultat = controlleur.ObtenirMatiere(1);

            // Assert
            Assert.IsNotNull(resultat);
            Assert.AreEqual(1, resultat.Id);
            Assert.AreEqual("420-137-GG", resultat.Code);
            Assert.AreEqual(90, resultat.NombreHeures);
        }

        // Test 3 : Matière inexistante retourne null
        [TestMethod]
        public void TestObtenirMatiere_IdInexistant_RetourneNull()
        {
            // Arrange
            Mock<IMatiereFictiveDAO> mockDao = new Mock<IMatiereFictiveDAO>();
            mockDao.Setup(d => d.GetById(999)).Returns((MatiereFictive?)null);

            MatiereFictiveControlleur controlleur = new MatiereFictiveControlleur(mockDao.Object);

            // Act
            MatiereFictive resultat = controlleur.ObtenirMatiere(999);

            // Assert
            Assert.IsNull(resultat);
        }
    }
}
