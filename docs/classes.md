# Classes
- Haverá as seguintes classes:
    - `Cursos`
    - `Turmas`
    - `Alunos`
- Todas as classes possuem um atributo `id` do tipo **int**. 
- Todas as classes possuem **contrutores vazios e com todos os paramêtros** 
- Todas as classes possuem uma **chave estrangeira ligando umas as outras**. 
- Todas as classes são **baseadas nas tabelas do banco de dados** com seus repectivos nomes. 
- Todos os campos com **id_OutraTabela** possuem uma chave estrangeira
    - **id_alunos** 
    - **id_turmas**
    - **id_cursos**
   
## Cursos 
- Existira 7 variaveis na classe
    - `id` sendo int
    - `curso` sendo do tipo String , indica nome do curso (ex: Curso: informatica )
    - `categoria` sendo do tipo String, indica o tipo do curso (ex: Categoria: Curso tecnico )
    - `aberto_para_incricao` sendo do tipo boolean, indica se o curso está aberto para inscrição (ex: Aberto para inscrição: Sim)
    - `carga_horaria` sendo do tipo int, indica o a caraga horario total para do curso em horas (ex: Carga Horária: 1200h)
    - `id_alunos` sendo uma chave estrangeira para a tabela alunos, indica os alunos do curso (ex: Alunos: {nome})
    - `id_turmas` sendo uma chave estrangeira para a tabela turmas, indica as turmas do curso (ex: Turmas: {numero})

## Turmas 
- Existira 6 variaveis na classe
    - `id` sendo int
    - `numero` sendo do tipo int, indica o número da turma (ex: Numero: {curso} 3)
    - `periodo` sendo do tipo String, indica o período da turma (ex: Periodo: manha)
    - `tempo_ativo` sendo do tipo String, indica o tempo ativo da turma (ex: Tempo percorrido: 1 ano)
    - `id_alunos` sendo uma chave estrangeira para a tabela alunos, indica os alunos da turma (ex: Alunos: {nome})
    - `id_cursos` sendo uma chave estrangeira para a tabela cursos, indica os cursos da turma (ex: Curso: informatica )

## Alunos 
- Existira 5 variaveis na classe
    - `id` sendo int
    - `nome` sendo do tipo String, indica o nome do aluno (ex: Nome: João Silva)
    - `idade` sendo do tipo int, indica a idade do aluno (ex: Idade: 20 anos)
    - `id_turmas` sendo uma chave estrangeira para a tabela turmas, indica a turma do aluno (ex: Turma: {curso} 3)
    - `id_cursos` sendo uma chave estrangeira para a tabela cursos, indica o curso do aluno (ex: Curso: informatica )
