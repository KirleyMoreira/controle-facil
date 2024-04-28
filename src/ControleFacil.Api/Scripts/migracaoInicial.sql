Build started...
Build succeeded.
The Entity Framework tools version '7.0.0' is older than that of the runtime '7.0.10'. Update the tools for the latest features and bug fixes. See https://aka.ms/AAc1fbw for more information.
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE usuario (
    "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
    "Email" text NOT NULL,
    "Senha" text NOT NULL,
    "DataCadastro" timestamp with time zone NOT NULL,
    "DataInativacao" timestamp with time zone NULL,
    CONSTRAINT "PK_usuario" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240420222837_migracaoInicial', '7.0.10');

COMMIT;


