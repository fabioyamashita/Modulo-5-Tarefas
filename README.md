# Módulo 5 - Técnicas de Programação I - C# - Tarefas

## Exercício com Filas (Queue Exercise - 08/08/2022)

Em um console app, crie um "sistema de gerenciamento de filas". Seu programa deve conter duas filas, uma prioritária e uma regular. Seu menu deve conter as seguintes opções:
- adicionar pessoa na fila regular;
- adicionar pessoa na fila prioritária;
- atender pessoa;

O sistema deve obedecer à seguinte regra: as pessoas na fila prioritária devem ser atendidas primeiramente, no entanto, a cada 3 atendimentos, obrigratoriamente uma pessoa da fila regular deve ser atendida (caso exista pessoas na fila).

## Exercício com LINQ I (LINQ_I - 10/08/2022)

O arquivo `selic.json` contém os dados históricos da taxa selic. Sendo um trimestre um conjunto de 3 meses, e considerando:
```
1º trimestre do ano -> de 01/01 até 31/03
2º trimestre do ano -> de 01/04 até 30/06
3º trimestre do ano -> de 01/07 até 30/09
4º trimestre do ano -> de 01/10 até 31/12
```
- Encontre o maior e o menor valor nominal da selic
- Qual o valor mais comum da selic?
- Qual valor médio?
- Encontre os meses em que houve mudança no valor da selic
- Calcule o valor médio de cada trimestre a partir de 2016
- Qual o valor mais alto e mais baixo da selic para cada presidente da república
- Durante a pandemia de covid, especificamente desde março/21, a selic está em constante subida. Calcule a taxa média de aumento nesse período. Dica: veja o método Zip

OBS: Utilize apenas LINQ para resolver os itens

## Exercício com LINQ II (LINQ_II - 10/08/2022)

Aqui você ira fazer vários "pequenos exercícios". Por favor, resolva todos no mesmo programa.

- Escreva um linq que retorne o caractere mais frequente de uma string qualquer (você define). Suponha que exista apenas um caracter mais frequente.
- Dada uma string não vazia consistindo apenas de caracteres especiais (!, @, # etc.), retorne um número (como uma string) onde cada dígito corresponde a determinado caractere especial no teclado ```( 1→ !, 2 → @, 3 → # etc.)```. Dica: monte uma string de correspondencia entre os números e os caracteres.
- Escreva um linq que embaralha uma lista ordenada. Por exemplo ```[1, 2, 3, 4, 5, 6, 7, 8, 9, 10] → [4, 9, 3, 5, 2, 10, 1, 6, 8, 7]```. Dica: Utilize o methodo Random
- Escreva um linq que transponha uma matriz quadrada (troque linhas por colunas). Dica: você aninhar Selects!
```
[1,1,1,1    [1,2,3,4
 2,2,2,2     1,2,3,4
 3,3,3,3   → 1,2,3,4
 4,4,4,4]    1,2,3,4]
 ```

## Fazendo um request HTTP (ConsumingAPI - 16/08/2022)
Conforme mostrado em sala, vamos utilizar a class `HttpClient` para fazer uma requisição http. (Esse não é o único método de se fazer chamadas http em C#, mas é o que vamos utilizar. Fique livre para explorar alternativas como o RestSharp ou até mesmo o WebClient)

Para este exercício, escolha uma API de sua preferência e faço um request para consumir seu conteúdo. Depois disso, selecione algumas informações que você considere relevante e faça alguma análise com elas. Fique livre para explorar sua criatividade.

Alguns exemplos de API:
 - Para os gamers saudosistas, [Poke API](https://pokeapi.co/)
 - Para os atletas de plantão, [Strava](https://developers.strava.com/). Dica: para a api do strava você precisa cadastrar seu "app" e gerar um token que deve ser enviado a cada request.
 - [Spotify](https://developer.spotify.com/documentation/web-api/quick-start/). Dica: mesmo esquema do strava, você deve registrar seu app e gerar um token
 - [publicapis](https://api.publicapis.org/entries) é uma api que lista apis publicas

## Projeto Final (FinalProjectAPI - 24/08/2022)

Para o projeto final você deve criar uma API utilizando o template de projeto 'ASP.NET Core Web API', conforme visto em sala.

Você deve criar 4 endpoints para efetuar as operações de CRUD e ao menos 1 endpoint para voltar alguma forma de análise, como por exemplo algum dado estatístico. Mas sinta-se livre para criar quantos você quiser =)

Você é livre para escolher sua base de dados (e.g.: pokemons, bandas, suas partidas lendárias de xadrez...). Além disso, você deve escolher uma forma de persistência de dados, fique a vontade para escolher salvar em um arquivo ou um banco de dados.

IMPORTANTE: você deve "carregar" toda base de dados e trabalhar as "consultas" usando LINQ. O objetivo não é treinar banco de dados, mas sim os conceitos vistos durante o módulo.

EXTRA: crie um projeto para consumir os dados da API que você acabou de construir. Kudos extras se você conseguir encaixar todos os conceitos vistos em sala, como: filas, pilhas, lambdas...

### Minha Solução

Desenvolvi um aplicativo Console para consumir uma API que criei usando as informações da API do Pokémon (https://pokeapi.co/docs/v2#pokemon). 

Primeiro, criei um projeto Web API e criei alguns EndPoints para retornar/modificar algumas informações:
1. Retornar todos os pokémons da lista
2. Retornar todos os pokémons da lista, de acordo com uma paginação que o usuário define
3. Procurar um Pokémon pelo ID
4. Procurar Pokémons através de uma busca (por letra ou palavra)
5. Retornar todos os pokémons da lista ordenados pelo nome
6. Inserir um novo Pokémon
7. Atualizar um Pokémon existente
8. Deletar um Pokémon

O banco de dados foi feito de 2 formas:
- FinalProjectAPI_JSON: Através de um arquivo JSON inicial, que contém os 151 pokémons iniciais (com Id, Nome e URL de acesso na API oficial)
- FinalProjectAPI_SQLServer: Através de um banco de dados vazio, que na primeira execução do programa é preenchido fazendo uma request para a API oficial, trazendo os 151 pokémons iniciais. Nesta parte foi usado o Entity Framework Core para fazer a ligação com o banco de dados.

Obs.: no item '6. Inserir um novo Pokémon', o pokémon é inserido apenas pelo número do ID. Assim, é feito um request para a API oficial e as outras informações são inseridas de acordo com a resposta que chega para o programa.

### Vídeo Demonstrativo
https://user-images.githubusercontent.com/98363297/186494796-16108993-19c3-4e47-a4ae-e9f31e3b9127.mp4

