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
