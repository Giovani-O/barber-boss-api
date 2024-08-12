## Overview
O objetivo deste projeto é o desenvolvimento de uma API completa para gerenciar e relatar a receita de uma ou mais barbearias. A API deve permitir a criação de usuários, bem como o login, visualização, edição e exclusão do mesmo, e sua senha deve ser criptografada. Cada usuário poderá ter uma ou mais barbearias cadastradas em seu nome, o que indica que deverá ser possível criar, visualizar, editar e remover barbearias. 
Em cada barbearia, será possível criar, visualizar, editar e excluir faturamentos (a receita de cada serviço prestado), e também deve ser possível gerar e exportar relatórios sob demanda com os faturamentos dos últimos 7 dias.
Além disso, a aplicação deverá contar com uma tratativa robusta de erros, filtros de exceções e testes unitários e de integração, e por fim, deve ser publicada utilizando o Azure.

## Requisitos funcionais
1. **Usuários**
	1. Cadastro, visualização, edição e exclusão
	2. Autenticaçã́o com JWT
	3. Criação e gerenciamento de barbearias
	4. Criação e gerenciamento de faturamentos
	5. Emissão de relatórios em excel e PDF
2. **Barbearia**
	1. Criação, visualização, edição e exclusão de barbearias
	2. Toda barbearia deve pertencer a um usuário
	3. Um usuário pode ter multiplas barbearias
3. **Faturamentos**
	1. Criação, visualização, edição e exclusão de faturamentos
	2. Todo faturamento deve pertencer a uma barbearia
	3. Uma barbearia pode ter multiplos faturamentos
	4. Todo faturamento tem um tipo de pagamento
4. **Tipos de pagamento**
	1. A criação de um faturamento pode ser diferende de acordo com o tipo de pagamento
	2. No pagamento a vista, será criada um faturamento na data escolhida
	3. No pagamento parcelado, serão criados faturamentos de acordo com o número de parcelas, onde as parcelas subsequentes terão suas datas de pagamento definidas com base na data de pagamento da primeira parcela.
5. **Relatórios**
	1. Emissão de relatórios de faturamentos do últimos 7 dias
	2. Deve ser emitido em excel e PDF
6. **Internacionalização**
	1. Criar arquivos de resource para disponibilizar  a API em português e inglês

## Requisitos não funcionais
1. **Documentação**
	1. Devem ser usados recursos como Swagger e summary para documentar toda a solução
2. **Clean architecture**
	1. Seguir um padrão de arquitetura limpa, como exposto no diagrama abaixo
3. **SOLID**
	1. Seguir os princípios SOLID durante todo o desenvolvimento
4. **DTOs**
	1. Todos os endpoints devem usar DTOs para receber ou devolver dados
5. **Cascade deletes**
	1. Devem ser feitos deletes em cascada, garantindo a integridade e confiabilidade do banco de dados
6. **Testes**
	1. Devem ser desenvolvidos testes unitários e de integração
7. **Deploy**
	1. A API deve ser hospedada no Azure
## Diagrama
Estes são os projetos que a solução deve ter e suas dependências, bem como qual projeto deverá se comunicar com o banco de dados. A seta indica de qual outro projeto um projeto é dependente, por exemplo, BarberBoss.Infrastructure depende de BarberBoss.Domain.
![image](https://github.com/user-attachments/assets/96a9a420-c766-47f5-83e9-c45ec2a4665e)


## Entidades
**User**
```Csharp
public class User {
	public Guid Id;
	public string Name;
	public string Email;
	public string Password;
}
```

**BarberShop**
```Csharp
public class BarberShop {
	public Guid Id;
	public string Name;
	public Guid UserId; // Foreign key
}
```

**Income**
```Csharp
public class Income {
	public Guid Id;
	public string Title;
	public string Description;
	public Datetime ServiceDate;
	public string PaymentType;
	public decimal Price;
	public Guid BarberShopId; // Foreign key
}
```

## Endpoints
##### User
1. **GET** /user
	1. Obtem todos os usuários
	2. Pode retornar
		1. 200 OK
		2. 400 Bad Request
2. **GET** /user/{id}
	1. Obtem um usuário por seu id, informado na URL
	2. Pode retornar
		1. 200 OK
		2. 400 Bad Request
		3. 404 Not Found
3. **POST** /user
	1. Adiciona um usuário, as informações virão no corpo da requisição
	2. Pode retornar
		3. 201 Created
		4. 400 Bad Request
4. **PUT** /user/{id}
	1. Edita um usuário por seu id, informado na URL. As novas informações virão no corpo da requisição
	2. Pode retornar
		1. 204 No Content
		2. 400 Bad Request
		3. 404 Not Found
5. **DELETE** /user/{id}
	1. Exclui um usuário por seu id, informado na URL
	2. Pode retornar
		1. 204 No Content
		2. 400 Bad Request
		3. 404 Not Found

##### Barber shop
1. **GET** /barber-shop/{userId}
	1. Obtem todas as barbearias de um usuário através de seu id, informado na URL
	2. Pode retornar
		1. 200 OK
		2. 400 Bad Request
		3. 404 Not Found
2. **GET** /barber-shop/{id}
	1. Obtem uma barbearia por seu id, informado na URL
	2. Pode retornar
		1. 200 OK
		2. 400 Bad Request
		3. 404 Not Found
3. **POST** /barber-shop
	1. Adiciona uma barbearia, as informações virão no corpo da requisição
	2. Pode retornar
		1. 201 Created
		2. 400 Bad Request
4. **PUT** /barber-shop/{id}
	1. Edita uma barbearia por seu id, informado na URL. As novas informações virão no corpo da requisição
	2. Pode retornar
		1. 204 No Content
		2. 400 Bad Request
		3. 404 Not Found
5. **DELETE** /barber-shop/{id}
	1. Exclui uma barbearia por seu id, informado na URL
	2. Pode retornar
		1. 204 No Content
		2. 400 Bad Request
		3. 404 Not Found

##### Income
1. **GET** /income/{barberShopId}
	1. Obtem todos os faturamentos de uma barbearia pelo id, informado na URL
	2. Pode retornar
		1. 200 OK
		2. 400 Bad Request
		3. 404 Not Found
2. **GET** /income/{id}
	1. Obtem um faturamento por seu id, informado na URL
	2. Pode retornar
		1. 200 OK
		2. 400 Bad Request
		3. 404 Not Found
3. **POST** /income
	1. Adiciona um faturamento, as informações virão no corpo da requisição
	2. Pode retornar
		1. 200 OK
		2. 400 Bad Request
4. **PUT** /income/{id}
	1. Edita um faturamento por seu id, informado na URL. As novas informações virão no corpo da requisição
	2. Pode retornar
		1. 204 No Content
		2. 400 Bad Request
		3. 404 Not Found
5. **DELETE** /income/{id}
	1. Exclui um faturamento por seu id, informado na URL
	2. Pode retornar
		1. 204 No Content
		2. 400 Bad Request
		3. 404 Not Found

##### Report
1. **GET** /report/excel/{barberShopId}
	1. Gera o relatório de faturamentos de uma barbearia nos últimos 7 dias no formato xlsx
	2. Pode retornar
		1. 200 OK
		2. 400 Bad Request
		3. 404 Not Found
2. **GET** /report/pdf/{barberShopId}
	1. Gera o relatório de faturamentos de uma barbearia nos últimos 7 dias no formato PDF
	2. Pode retornar
		1. 200 OK
		2. 400 Bad Request
		3. 404 Not Found
