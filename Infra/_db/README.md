Script de criação das tabelas:
```
CREATE TABLE Produto (
    Id  INTEGER PRIMARY KEY AUTOINCREMENT,
    Nome VARCHAR(255) NOT NULL,             
    Descricao TEXT,                         
    Preco DECIMAL(10, 2) NOT NULL,          
    Estoque INT,                   
    ImagemUrl VARCHAR(255),                 
    DataCadastro DATETIME DEFAULT CURRENT_TIMESTAMP
);

```