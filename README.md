# ewave-livraria-pleno-II

 **Desenvolvedor Back-End C#**  
 
 ## Desafio

A editora "To do" é uma empresa multinacional que atua no seguimento de venda de livros físicos, possui mais de 20 lojas espalhadas pelo mundo, seu sucesso é devido a qualidade de atendimento e atualização constante de sua biblioteca com rapidez e dedicação em tudo o que fazem. 

Com esse enorme sucesso, se propos a ajudar as instituições de ensino com emprestimos de seu variado catalogo de livros para os Estudantes. 
Tendo em vista essa necessidade, foi visto a necessidade de desenvolver um sistema que controla-se esses emprestimos

 ## Regras

* Todo usuario pode emprestar no maximo 2 livros.
* Todo emprestimo deve ser no maximo de 30 dias para cada livro.
* Informar ao Administrador do Sistema caso um livro extrapole o prazo máximo de dias emprestado.
* O Usuário que infringir a regra dos dias fica impossibilitado de emprestar qualquer outro livro até a devolução e só poderá emprestar novamente após 30 dias.
* Livros emprestados deverão estar indisponiveis para outros Usuários.
* Permitir reservar livros. 
 
 
 
 ## Soluçaõ
 
Na abordagem adotada para poder fornecer uma solução de computador para a biblioteca, era criar um aplicativo da Web, determinando dois tipos de usuário (roles).
- Administrador, no comando dos emprestimos / devolução dos livros.
- Leitor, ele pode reservar os livros, para depois pegarlos com o administrador.

As ferramentas informaticas, linguagem, metodologias e estructura a usar foram:
 
- ASp.net Core c#
- SqlServer Xpress para BD
- Estructura baseada no DDD
- Bootrap,
- Docs Swagger para webapi 

Para a criação da base de datos, usei o CODE FIRST.
 
 ### Problemas
 
 - Perder muito tempo, ao não poder concluir o tema dos roles.
 - O CODE FIRST, gera as tabelas em dois tempos, (por os dois dbcontext).

### Resolução as problemas.

- Usar dois dbcontext.
- Usar o script para gerar a BD.

### usuario de prueba
usuario: 	ewave@teste.com 
password:	Asd123. 
 
 
 
 
 
 





  

