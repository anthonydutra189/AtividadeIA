using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AtividadeIA
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int? IdTurmas { get; set; }
        public int? IdCursos { get; set; }

        public Aluno() { }

        public Aluno(int id, string nome, int idade, int? idTurmas, int? idCursos)
        {
            Id = id;
            Nome = nome;
            Idade = idade;
            IdTurmas = idTurmas;
            IdCursos = idCursos;
        }

        public void Mostrar()
        {
            Console.WriteLine($"ID: {Id} | Nome: {Nome} | Idade: {Idade} anos | Turma (ID): {IdTurmas} | Curso (ID): {IdCursos}");
        }

        public static List<Aluno> ListarTodos()
        {
            List<Aluno> alunos = new List<Aluno>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM alunos";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        alunos.Add(MapearDoBanco(reader));
                    }
                }
            }
            return alunos;
        }

        public static Aluno? ListarPorId(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM alunos WHERE id = @id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapearDoBanco(reader);
                        }
                    }
                }
            }
            return null;
        }

        public void Adicionar()
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = @"INSERT INTO alunos (nome, idade, id_turmas, id_cursos) 
                                 VALUES (@nome, @idade, @id_turmas, @id_cursos)";
                using (var command = new MySqlCommand(query, connection))
                {
                    PreencherParametros(command);
                    command.ExecuteNonQuery();
                    Id = (int)command.LastInsertedId;
                    Console.WriteLine("Aluno adicionado com sucesso!");
                }
            }
        }

        public static void ExcluirPorId(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM alunos WHERE id = @id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int afetados = command.ExecuteNonQuery();
                    if (afetados > 0)
                        Console.WriteLine("Aluno excluído com sucesso!");
                    else
                        Console.WriteLine("Aluno não encontrado.");
                }
            }
        }

        public void Atualizar()
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = @"UPDATE alunos SET nome = @nome, idade = @idade, 
                                 id_turmas = @id_turmas, id_cursos = @id_cursos WHERE id = @id";
                using (var command = new MySqlCommand(query, connection))
                {
                    PreencherParametros(command);
                    command.Parameters.AddWithValue("@id", Id);
                    int afetados = command.ExecuteNonQuery();
                    if (afetados > 0)
                        Console.WriteLine("Aluno atualizado com sucesso!");
                    else
                        Console.WriteLine("Aluno não encontrado para atualização.");
                }
            }
        }

        private static Aluno MapearDoBanco(MySqlDataReader reader)
        {
            return new Aluno
            {
                Id = reader.GetInt32("id"),
                Nome = reader.GetString("nome"),
                Idade = reader.GetInt32("idade"),
                IdTurmas = reader.IsDBNull(reader.GetOrdinal("id_turmas")) ? (int?)null : reader.GetInt32("id_turmas"),
                IdCursos = reader.IsDBNull(reader.GetOrdinal("id_cursos")) ? (int?)null : reader.GetInt32("id_cursos")
            };
        }

        private void PreencherParametros(MySqlCommand command)
        {
            command.Parameters.AddWithValue("@nome", Nome);
            command.Parameters.AddWithValue("@idade", Idade);
            command.Parameters.AddWithValue("@id_turmas", (object)IdTurmas ?? DBNull.Value);
            command.Parameters.AddWithValue("@id_cursos", (object)IdCursos ?? DBNull.Value);
        }
    }
}
