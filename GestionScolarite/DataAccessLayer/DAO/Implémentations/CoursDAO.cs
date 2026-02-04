using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.DataAccessLayer.DataAccess;
using GestionScolarite.ModelLayer;
using Microsoft.Data.SqlClient;
namespace GestionScolarite.DataAccessLayer.DAO.Implémentations;

internal class CoursDAO : ICoursDAO
{
    public void Ajouter(Cours cours)
    {
        using (SqlConnection conn = DatabaseConnection.GetConnection())
        {
            conn.Open();
            var requete = "INSERT INTO Cours (Titre, Code) VALUES (@Titre, @Code)";
            using (var cmd = new SqlCommand(requete, conn))
            {
                cmd.Parameters.AddWithValue("@Titre", cours.Titre);
                cmd.Parameters.AddWithValue("@Code", cours.Code);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Modifier(Cours cours)
    {
        using (SqlConnection conn = DatabaseConnection.GetConnection())
        {
            conn.Open();
            var requete = "UPDATE Cours SET Titre = @Titre, Code = @Code WHERE Id = @Id";
            using (var cmd = new SqlCommand(requete, conn))
            {
                cmd.Parameters.AddWithValue("@Titre", cours.Titre);
                cmd.Parameters.AddWithValue("@Code", cours.Code);
                cmd.Parameters.AddWithValue("@Id", cours.Id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Supprimer(int id)
    {
        using (SqlConnection conn = DatabaseConnection.GetConnection())
        {
            conn.Open();
            var requete = "DELETE FROM Cours WHERE Id = @Id";
            using (var cmd = new SqlCommand(requete, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public Cours? GetById(int id)
    {
        using (SqlConnection conn = DatabaseConnection.GetConnection())
        {
            conn.Open();
            var requete = "SELECT * FROM Cours WHERE Id = @Id";
            using (var cmd = new SqlCommand(requete, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Cours(
                            (int)reader["Id"],
                            (string)reader["Titre"],
                            (string)reader["Code"]
                        );
                    }
                }
            }
        }
        return null;
    }

    public List<Cours> GetAll()
    {
        var liste = new List<Cours>();
        using (SqlConnection conn = DatabaseConnection.GetConnection())
        {
            conn.Open();
            var requete = "SELECT * FROM Cours";
            using (var cmd = new SqlCommand(requete, conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    liste.Add(new Cours(
                        (int)reader["Id"],
                        (string)reader["Titre"],
                        (string)reader["Code"]
                    ));
                }
            }
        }
        return liste;
    }

    public Cours? GetByCode(string code)
    {
        using (SqlConnection conn = DatabaseConnection.GetConnection())
        {
            conn.Open();
            var requete = "SELECT * FROM Cours WHERE Code = @Code";
            using (var cmd = new SqlCommand(requete, conn))
            {
                cmd.Parameters.AddWithValue("@Code", code);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Cours(
                            (int)reader["Id"],
                            (string)reader["Titre"],
                            (string)reader["Code"]
                        );
                    }
                }
            }
        }
        return null;
    }
}

