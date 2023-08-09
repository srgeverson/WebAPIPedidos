/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.0.35
 * Time: 15/08/2022 13:33:31
 ************************************************************/

--DB Principal
USE [master];
GO
--Apagando banco de dados se existir
DROP DATABASE 
IF EXISTS [db_teste];
GO
CREATE DATABASE [db_teste];  
GO 
--Acessando DB
USE [db_teste]
GO

CREATE LOGIN user_teste
    WITH PASSWORD = '@G12345678';  
GO 

CREATE USER [user_teste] FOR LOGIN [user_teste]
GO
--Criando as permissões
GRANT INSERT TO [user_teste];
GO
GRANT DELETE TO [user_teste];
GO
GRANT UPDATE TO [user_teste];
GO
GRANT SELECT TO [user_teste];
GO
GRANT EXECUTE TO [user_teste];
GO
GRANT ALTER TO [user_teste];
GO
GRANT CREATE TABLE TO [user_teste];
GO
GRANT CREATE FUNCTION TO [user_teste];
GO
GRANT CREATE PROCEDURE TO [user_teste];
GO
GRANT CREATE VIEW TO [user_teste];
GO
GRANT REFERENCES TO [user_teste];
GO