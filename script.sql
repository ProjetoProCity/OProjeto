CREATE DATABASE bdEcommerce;
USE bdEcommerce;

CREATE TABLE Usuario(
	Id int primary key auto_increment,
    Nome varchar(50) not null,
    Email varchar(50) not null,
    Senha varchar(50) not null
);

CREATE TABLE Cliente(
	CodCli int primary key auto_increment,
    NomeCli varchar(50) not null,
    TelCli varchar(20) not null,
    EmailCli varchar(50) not null
);

create table produto(
	Id int primary key auto_increment,
    Prod varchar(40) not null,
    Descr varchar(200) not null,
	Qtd int not null,
	Preco double not null
);

SELECT * FROM Usuario;
SELECT * FROM Cliente;
select * from produto;