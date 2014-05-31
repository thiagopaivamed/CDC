
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/26/2014 15:22:02
-- Generated from EDMX file: c:\users\thiago\documents\visual studio 2013\Projects\CDC\Dominio\Models\Tabelas.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CDC];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'TipodeProduto'
CREATE TABLE [dbo].[TipodeProduto] (
    [IdTipoProduto] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Produto'
CREATE TABLE [dbo].[Produto] (
    [IdProduto] int IDENTITY(1,1) NOT NULL,
    [Descricao] nvarchar(max)  NOT NULL,
    [Imagem] nvarchar(max)  NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [Peso] int  NOT NULL,
    [Preco] decimal  NOT NULL,
    [Tipo] int  NOT NULL
);
GO

-- Creating table 'ItemCarrinho'
CREATE TABLE [dbo].[ItemCarrinho] (
    [IdItemCarrinho] int IDENTITY(1,1) NOT NULL,
    [Produtos] int  NOT NULL,
    [Quantidade] int  NOT NULL
);
GO

-- Creating table 'Carrinho'
CREATE TABLE [dbo].[Carrinho] (
    [IdCarrinho] int IDENTITY(1,1) NOT NULL,
    [DataHora] datetime  NOT NULL,
    [ItensCarrinho] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdTipoProduto] in table 'TipodeProduto'
ALTER TABLE [dbo].[TipodeProduto]
ADD CONSTRAINT [PK_TipodeProduto]
    PRIMARY KEY CLUSTERED ([IdTipoProduto] ASC);
GO

-- Creating primary key on [IdProduto] in table 'Produto'
ALTER TABLE [dbo].[Produto]
ADD CONSTRAINT [PK_Produto]
    PRIMARY KEY CLUSTERED ([IdProduto] ASC);
GO

-- Creating primary key on [IdItemCarrinho] in table 'ItemCarrinho'
ALTER TABLE [dbo].[ItemCarrinho]
ADD CONSTRAINT [PK_ItemCarrinho]
    PRIMARY KEY CLUSTERED ([IdItemCarrinho] ASC);
GO

-- Creating primary key on [IdCarrinho] in table 'Carrinho'
ALTER TABLE [dbo].[Carrinho]
ADD CONSTRAINT [PK_Carrinho]
    PRIMARY KEY CLUSTERED ([IdCarrinho] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Tipo] in table 'Produto'
ALTER TABLE [dbo].[Produto]
ADD CONSTRAINT [FK_TipodeProdutoProduto]
    FOREIGN KEY ([Tipo])
    REFERENCES [dbo].[TipodeProduto]
        ([IdTipoProduto])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TipodeProdutoProduto'
CREATE INDEX [IX_FK_TipodeProdutoProduto]
ON [dbo].[Produto]
    ([Tipo]);
GO

-- Creating foreign key on [Produtos] in table 'ItemCarrinho'
ALTER TABLE [dbo].[ItemCarrinho]
ADD CONSTRAINT [FK_ProdutoItemCarrinho]
    FOREIGN KEY ([Produtos])
    REFERENCES [dbo].[Produto]
        ([IdProduto])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProdutoItemCarrinho'
CREATE INDEX [IX_FK_ProdutoItemCarrinho]
ON [dbo].[ItemCarrinho]
    ([Produtos]);
GO

-- Creating foreign key on [ItensCarrinho] in table 'Carrinho'
ALTER TABLE [dbo].[Carrinho]
ADD CONSTRAINT [FK_ItemCarrinhoCarrinho]
    FOREIGN KEY ([ItensCarrinho])
    REFERENCES [dbo].[ItemCarrinho]
        ([IdItemCarrinho])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemCarrinhoCarrinho'
CREATE INDEX [IX_FK_ItemCarrinhoCarrinho]
ON [dbo].[Carrinho]
    ([ItensCarrinho]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------