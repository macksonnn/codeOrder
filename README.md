

Tecnologias Utilizadas
.NET Core 7.0
xUnit (para testes unitários)

Configuração e Execução

2. Configurar o Projeto Principal
2.1. Compilar o Projeto
Navegue até a pasta do projeto principal e compile:
cd CodeChallengeApplication
dotnet build

2.2. Criar o Arquivo de Entrada
Crie um arquivo chamado input.csv com os dados dos pedidos de reembolso no seguinte formato:
Id,TipoProcedimento,DataProcedimento,ValorPago,ClienteId
1,Consulta Médica,2024-09-15,200.00,123
2,Exame de Imagem,2024-09-20,250.00,123
3,Exame Laboratorial,2024-09-25,150.00,124
4,Outro,2024-08-01,300.00,125
5,Consulta Médica,2024-07-10,50.00,126
PS: ja existe um, para ser usado como exemmplo.
*******Coloque o arquivo na mesma pasta do projeto ou em um diretório acessível.********

2.3. Executar o Projeto
Execute o projeto passando o arquivo de entrada input.csv como entrada e salvando os resultados em output.csv:
dotnet run < input.csv > output.csv

2.4. Verificar os Resultados
Confira os resultados processados no arquivo output.csv.

3. Configurar e Executar os Testes
3.1. Configurar o Projeto de Testes
Navegue para a pasta do projeto de testes e compile:
cd ../CodeChallengeApplication.Tests
dotnet build

3.2. Executar os Testes
Execute os testes unitários para validar as regras de negócio:
dotnet test

3.3. Verificar Resultados dos Testes
Se os testes passarem, você verá algo como:
Passed! 7 tests passed.

Estrutura do Projeto
CodeChallengeApplication/
├── Program.cs               # Entrada principal da aplicação
├── Models/
│   ├── OrderEntity.cs       # Representa os pedidos de reembolso
│   ├── OrderResult.cs       # Representa os resultados processados
├── Business/
│   ├── OrderBusiness.cs     # Contém as regras de negócio
└── Tests/
    ├── OrderBusinessTests.cs # Testes unitários das regras de negócio


Comandos Úteis
Executar o Projeto Principal:
dotnet run < input.csv > output.csv

Executar os Testes
dotnet test


