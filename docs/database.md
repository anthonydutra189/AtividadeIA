# Banco de dados
- Estou utilizando um banco de dados **MySQL** pelo **XAMP** com a interface **phpMyAdmin**
- Estas são as minhas informações de conexão com o banco de dados:
    - Nome do banco: `bd_escola`
    - porta: `3307`
    - usuário: `root`
    - senha: `Não possue`
    - link: [http://localhost/phpmyadmin/index.php]
    - Servidor: 127.0.0.1:3307 
    - Tabelas: `alunos`, `turmas`, `cursos`

- Conecte esse banco de dados com o meu projeto em C#
- Crie uma classe/arquivo diferente para cada classe/tabela do banco de dados
- Crie contrutores para cada tabela (de forma que não seja necessario colocar a chave estrangeira)
- Para cada Tabela/classe crie um metodo/funções
    - Liste todos os registros da tabela
    - Liste os registros por id
    - Adicionar registro a tabela
    - excluir registros da tabela por id
    - atualizar registros da tabela por id
- Crie uma inteface utilizando `console.write` para exibir as informações na tela

## Filtros 
- Filtre qualquer dado que não seja diferente do campo ex: nome: 123 console.write(dado invalido)
- Filtre qualquer dado repetido na tabela cursos (não pode haver 2 cursos iguais mas sim de periodos e turmas diferentes) e o numero da turma não pode se repetir (ex: informatica 1 já existe mas informatica 2 não )
- Os alunos não podem se repetir na mesma tabela (não pode haver 2 alunos iguais mas sim de turmas e cursos diferentes filtragem deve ser feita pelo id não por nome )

# Tabelas

- Cursos
    - `id`
    - `curso`
    - `categoria`
    - `aberto_para_incricao`
    - `carga_horaria`
    - `id_alunos`
    - `id_turmas`

- Turmas
    - `id`
    - `numero`
    - `periodo`
    - `tempo_ativo`
    - `id_alunos`
    - `id_cursos`

- Alunos
    - `id`
    - `nome`
    - `idade`
    - `id_turmas`
    - `id_cursos`
    
- É possivel que exista alunos sem curso e sem turma


- Todas as tabelas possuem uma **chave estrangeira** ligando umas as outras 
    - id_alunos `fk_alunos_turmas`  , `fk_alunos_cursos` 
    - id_turmas `fk_turmas_alunos` , `fk_turmas_cursos` 
    - id_cursos `fk_cursos_alunos` , `fk_cursos_turmas` 
