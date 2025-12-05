CREATE OR ALTER PROCEDURE contagemRegistros
(
    @cNmTabela VARCHAR(100)
)
AS
BEGIN
    DECLARE @cComandoSQL NVARCHAR(MAX),
            @cResultado  INT;

    -- Verifica se a tabela existe no banco atual
    IF OBJECT_ID(@cNmTabela, 'U') IS NOT NULL
    BEGIN
        SET @cComandoSQL = N'SELECT @iContagem = COUNT(*) FROM ' + QUOTENAME(@cNmTabela);

        EXEC sp_executesql 
            @cComandoSQL,
            N'@iContagem INT OUTPUT',
            @iContagem = @cResultado OUTPUT;

        SELECT @cResultado AS iContagem;
    END
    ELSE
    BEGIN
        -- Caso a tabela não exista, retorna 0 ou uma mensagem
        SELECT -1 AS iContagem, 'Tabela não encontrada' AS Mensagem;
    END
END