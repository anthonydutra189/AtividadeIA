using System;
using System.Collections.Generic;

namespace AtividadeIA
{
    class Program
    {
        static void Main(string[] args)
        {
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("\n--- Sistema Escolar ---");
                Console.WriteLine("1. Gerenciar Cursos");
                Console.WriteLine("2. Gerenciar Turmas");
                Console.WriteLine("3. Gerenciar Alunos");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        MenuCursos();
                        break;
                    case "2":
                        MenuTurmas();
                        break;
                    case "3":
                        MenuAlunos();
                        break;
                    case "0":
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        static void MenuCursos()
        {
            Console.WriteLine("\n--- Cursos ---");
            Console.WriteLine("1. Listar todos");
            Console.WriteLine("2. Buscar por ID");
            Console.WriteLine("3. Adicionar");
            Console.WriteLine("4. Atualizar");
            Console.WriteLine("5. Excluir");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine() ?? "";

            try
            {
                switch (opcao)
                {
                    case "1":
                        var cursos = Curso.ListarTodos();
                        foreach (var c in cursos) c.Mostrar();
                        break;
                    case "2":
                        Console.Write("ID do Curso: ");
                        int idBusca = int.Parse(Console.ReadLine() ?? "0");
                        var cursoEncontrado = Curso.ListarPorId(idBusca);
                        if (cursoEncontrado != null) cursoEncontrado.Mostrar();
                        else Console.WriteLine("Curso não encontrado.");
                        break;
                    case "3":
                        Curso novoCurso = new Curso();
                        Console.Write("Nome do Curso: ");
                        string nome = Console.ReadLine() ?? "";
                        if (int.TryParse(nome, out _)) { Console.WriteLine("Dado inválido."); return; }
                        novoCurso.NomeCurso = nome;
                        Console.Write("Categoria: ");
                        novoCurso.Categoria = Console.ReadLine() ?? "";
                        Console.Write("Aberto para inscrição (true/false): ");
                        novoCurso.AbertoParaInscricao = bool.Parse(Console.ReadLine() ?? "false");
                        Console.Write("Carga Horária (horas): ");
                        novoCurso.CargaHoraria = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("ID Alunos (Deixe vazio se nulo): ");
                        string idAlunosStr = Console.ReadLine() ?? "";
                        novoCurso.IdAlunos = string.IsNullOrEmpty(idAlunosStr) ? (int?)null : int.Parse(idAlunosStr);
                        Console.Write("ID Turmas (Deixe vazio se nulo): ");
                        string idTurmasStr = Console.ReadLine() ?? "";
                        novoCurso.IdTurmas = string.IsNullOrEmpty(idTurmasStr) ? (int?)null : int.Parse(idTurmasStr);
                        novoCurso.Adicionar();
                        break;
                    case "4":
                        Console.Write("ID do Curso a atualizar: ");
                        int idAtualizar = int.Parse(Console.ReadLine() ?? "0");
                        var cursoAtualizar = Curso.ListarPorId(idAtualizar);
                        if (cursoAtualizar != null)
                        {
                            Console.Write("Novo Nome do Curso: ");
                            string nNome = Console.ReadLine() ?? "";
                            if (int.TryParse(nNome, out _)) { Console.WriteLine("Dado inválido."); return; }
                            cursoAtualizar.NomeCurso = nNome;
                            Console.Write("Nova Categoria: ");
                            cursoAtualizar.Categoria = Console.ReadLine() ?? "";
                            Console.Write("Aberto para inscrição (true/false): ");
                            cursoAtualizar.AbertoParaInscricao = bool.Parse(Console.ReadLine() ?? "false");
                            Console.Write("Nova Carga Horária: ");
                            cursoAtualizar.CargaHoraria = int.Parse(Console.ReadLine() ?? "0");
                            Console.Write("Novo ID Alunos: ");
                            string nIdAlunos = Console.ReadLine() ?? "";
                            cursoAtualizar.IdAlunos = string.IsNullOrEmpty(nIdAlunos) ? (int?)null : int.Parse(nIdAlunos);
                            Console.Write("Novo ID Turmas: ");
                            string nIdTurmas = Console.ReadLine() ?? "";
                            cursoAtualizar.IdTurmas = string.IsNullOrEmpty(nIdTurmas) ? (int?)null : int.Parse(nIdTurmas);
                            cursoAtualizar.Atualizar();
                        }
                        else { Console.WriteLine("Curso não encontrado."); }
                        break;
                    case "5":
                        Console.Write("ID do Curso a excluir: ");
                        int idExcluir = int.Parse(Console.ReadLine() ?? "0");
                        Curso.ExcluirPorId(idExcluir);
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: Dado inválido. Detalhes: {ex.Message}");
            }
        }

        static void MenuTurmas()
        {
            Console.WriteLine("\n--- Turmas ---");
            Console.WriteLine("1. Listar todas");
            Console.WriteLine("2. Buscar por ID");
            Console.WriteLine("3. Adicionar");
            Console.WriteLine("4. Atualizar");
            Console.WriteLine("5. Excluir");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine() ?? "";

            try
            {
                switch (opcao)
                {
                    case "1":
                        var turmas = Turma.ListarTodos();
                        foreach (var t in turmas) t.Mostrar();
                        break;
                    case "2":
                        Console.Write("ID da Turma: ");
                        int idBusca = int.Parse(Console.ReadLine() ?? "0");
                        var turmaEncontrada = Turma.ListarPorId(idBusca);
                        if (turmaEncontrada != null) turmaEncontrada.Mostrar();
                        else Console.WriteLine("Turma não encontrada.");
                        break;
                    case "3":
                        Turma novaTurma = new Turma();
                        Console.Write("Número da Turma: ");
                        novaTurma.Numero = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("Período: ");
                        string periodo = Console.ReadLine() ?? "";
                        if (int.TryParse(periodo, out _)) { Console.WriteLine("Dado inválido."); return; }
                        novaTurma.Periodo = periodo;
                        Console.Write("Tempo Ativo: ");
                        novaTurma.TempoAtivo = Console.ReadLine() ?? "";
                        Console.Write("ID Alunos (Deixe vazio se nulo): ");
                        string idAlunosStr = Console.ReadLine() ?? "";
                        novaTurma.IdAlunos = string.IsNullOrEmpty(idAlunosStr) ? (int?)null : int.Parse(idAlunosStr);
                        Console.Write("ID Cursos (Deixe vazio se nulo): ");
                        string idCursosStr = Console.ReadLine() ?? "";
                        novaTurma.IdCursos = string.IsNullOrEmpty(idCursosStr) ? (int?)null : int.Parse(idCursosStr);
                        novaTurma.Adicionar();
                        break;
                    case "4":
                        Console.Write("ID da Turma a atualizar: ");
                        int idAtualizar = int.Parse(Console.ReadLine() ?? "0");
                        var turmaAtualizar = Turma.ListarPorId(idAtualizar);
                        if (turmaAtualizar != null)
                        {
                            Console.Write("Novo Número da Turma: ");
                            turmaAtualizar.Numero = int.Parse(Console.ReadLine() ?? "0");
                            Console.Write("Novo Período: ");
                            string nPeriodo = Console.ReadLine() ?? "";
                            if (int.TryParse(nPeriodo, out _)) { Console.WriteLine("Dado inválido."); return; }
                            turmaAtualizar.Periodo = nPeriodo;
                            Console.Write("Novo Tempo Ativo: ");
                            turmaAtualizar.TempoAtivo = Console.ReadLine() ?? "";
                            Console.Write("Novo ID Alunos: ");
                            string nIdAlunos = Console.ReadLine() ?? "";
                            turmaAtualizar.IdAlunos = string.IsNullOrEmpty(nIdAlunos) ? (int?)null : int.Parse(nIdAlunos);
                            Console.Write("Novo ID Cursos: ");
                            string nIdCursos = Console.ReadLine() ?? "";
                            turmaAtualizar.IdCursos = string.IsNullOrEmpty(nIdCursos) ? (int?)null : int.Parse(nIdCursos);
                            turmaAtualizar.Atualizar();
                        }
                        else { Console.WriteLine("Turma não encontrada."); }
                        break;
                    case "5":
                        Console.Write("ID da Turma a excluir: ");
                        int idExcluir = int.Parse(Console.ReadLine() ?? "0");
                        Turma.ExcluirPorId(idExcluir);
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: Dado inválido. Detalhes: {ex.Message}");
            }
        }

        static void MenuAlunos()
        {
            Console.WriteLine("\n--- Alunos ---");
            Console.WriteLine("1. Listar todos");
            Console.WriteLine("2. Buscar por ID");
            Console.WriteLine("3. Adicionar");
            Console.WriteLine("4. Atualizar");
            Console.WriteLine("5. Excluir");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine() ?? "";

            try
            {
                switch (opcao)
                {
                    case "1":
                        var alunos = Aluno.ListarTodos();
                        foreach (var a in alunos) a.Mostrar();
                        break;
                    case "2":
                        Console.Write("ID do Aluno: ");
                        int idBusca = int.Parse(Console.ReadLine() ?? "0");
                        var alunoEncontrado = Aluno.ListarPorId(idBusca);
                        if (alunoEncontrado != null) alunoEncontrado.Mostrar();
                        else Console.WriteLine("Aluno não encontrado.");
                        break;
                    case "3":
                        Aluno novoAluno = new Aluno();
                        Console.Write("Nome do Aluno: ");
                        string nome = Console.ReadLine() ?? "";
                        if (int.TryParse(nome, out _)) { Console.WriteLine("Dado inválido."); return; }
                        novoAluno.Nome = nome;
                        Console.Write("Idade: ");
                        novoAluno.Idade = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("ID Turmas (Deixe vazio se nulo): ");
                        string idTurmasStr = Console.ReadLine() ?? "";
                        novoAluno.IdTurmas = string.IsNullOrEmpty(idTurmasStr) ? (int?)null : int.Parse(idTurmasStr);
                        Console.Write("ID Cursos (Deixe vazio se nulo): ");
                        string idCursosStr = Console.ReadLine() ?? "";
                        novoAluno.IdCursos = string.IsNullOrEmpty(idCursosStr) ? (int?)null : int.Parse(idCursosStr);
                        novoAluno.Adicionar();
                        break;
                    case "4":
                        Console.Write("ID do Aluno a atualizar: ");
                        int idAtualizar = int.Parse(Console.ReadLine() ?? "0");
                        var alunoAtualizar = Aluno.ListarPorId(idAtualizar);
                        if (alunoAtualizar != null)
                        {
                            Console.Write("Novo Nome do Aluno: ");
                            string nNome = Console.ReadLine() ?? "";
                            if (int.TryParse(nNome, out _)) { Console.WriteLine("Dado inválido."); return; }
                            alunoAtualizar.Nome = nNome;
                            Console.Write("Nova Idade: ");
                            alunoAtualizar.Idade = int.Parse(Console.ReadLine() ?? "0");
                            Console.Write("Novo ID Turmas: ");
                            string nIdTurmas = Console.ReadLine() ?? "";
                            alunoAtualizar.IdTurmas = string.IsNullOrEmpty(nIdTurmas) ? (int?)null : int.Parse(nIdTurmas);
                            Console.Write("Novo ID Cursos: ");
                            string nIdCursos = Console.ReadLine() ?? "";
                            alunoAtualizar.IdCursos = string.IsNullOrEmpty(nIdCursos) ? (int?)null : int.Parse(nIdCursos);
                            alunoAtualizar.Atualizar();
                        }
                        else { Console.WriteLine("Aluno não encontrado."); }
                        break;
                    case "5":
                        Console.Write("ID do Aluno a excluir: ");
                        int idExcluir = int.Parse(Console.ReadLine() ?? "0");
                        Aluno.ExcluirPorId(idExcluir);
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: Dado inválido. Detalhes: {ex.Message}");
            }
        }
    }
}