# FisioAppConsole

Este projeto em C# é um **sistema de gerenciamento de pacientes** para um **fisioterapeuta**, rodando diretamente no **console** (sem interface gráfica).

## Sumário
- [Descrição](#descrição)
- [Funcionalidades](#funcionalidades)
- [Pré-Requisitos](#pré-requisitos)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Como Usar](#como-usar)
- [Exemplo de Execução](#exemplo-de-execução)
- [Observações](#observações)

---

## Descrição

O **FisioAppConsole** permite **cadastrar**, **editar**, **excluir** e **visualizar** pacientes, além de registrar sessões de fisioterapia. O sistema armazena tudo em um arquivo JSON (`pacientes.json`) criado automaticamente na pasta de execução.

**Diferencial**: O **ID** do paciente é **gerado automaticamente** com base no **menor número** disponível.
- Se nenhum paciente existir, o primeiro cadastro será `ID = 1`.
- Se os IDs existentes são `[2, 3, 4]`, o próximo será `1`.
- Se os IDs existentes são `[1, 2, 3]`, o próximo será `4`.

---

## Funcionalidades

1. **Cadastrar Paciente**  
   - Cria um novo registro com **ID automático**, **nome**, **idade** e **diagnóstico**.

2. **Registrar Sessão**  
   - Adiciona observações no histórico de sessões do paciente.

3. **Editar Paciente**  
   - Modifica nome, idade ou diagnóstico de um paciente existente.

4. **Excluir Paciente**  
   - Remove permanentemente um paciente do cadastro (após confirmação).

5. **Listar Pacientes**  
   - Exibe todos os pacientes em **ordem crescente** de ID.

6. **Visualizar Histórico de Sessões**  
   - Mostra as observações de cada sessão registrada.

7. **Sair**  
   - Encerra o programa.

---

## Pré-Requisitos

- **.NET 6** (ou superior) instalado em seu ambiente.
- Qualquer **sistema operacional** compatível com .NET Core/6+ (Windows, Linux, macOS).

---

## Estrutura do Projeto

```bash
FisioAppConsole
└── Program.cs
```

- **`Program.cs`**: Contém toda a lógica do aplicativo (modelos, persistência em JSON e as opções do menu de console).

Quando executado, um arquivo **`pacientes.json`** é criado/atualizado no **diretório de execução** para persistência de dados.

---

## Como Usar

1. **Clonar** ou **baixar** os arquivos do projeto.
2. **Abrir** o terminal na pasta do projeto.
3. **Compilar** e **executar**:
   ```bash
   dotnet run
   ```
   - Isto criará/lerá o arquivo `pacientes.json` na mesma pasta do executável, sempre que o programa for executado.

> Se preferir, abra o projeto no **Visual Studio** ou **VS Code** (com extensão C#) e clique em **Run**/**Start**.

---

## Exemplo de Execução

Ao rodar, aparece o menu principal:

```text
=== Sistema de Gerenciamento de Pacientes (Console) ===
1. Sair
2. Cadastrar Paciente
3. Registrar Sessão
4. Editar Paciente
5. Excluir Paciente
6. Listar Pacientes
7. Visualizar Histórico de Sessões
Escolha uma opção:
```

- **Cadastrar Paciente** (opção 2):  
  - ID é **automático** (não perguntado).
  - Você digita **nome**, **idade** e **diagnóstico**.
  - O programa salva no `pacientes.json`.

- **Listar Pacientes** (opção 6):  
  - Mostra todos os pacientes **ordenados** por ID.

- **Visualizar Histórico** (opção 7):  
  - Mostra o histórico de sessões registrado para o ID informado.

---

## Observações

- Para **editar** ou **excluir**, você informa o **ID** do paciente em questão.
- Se o **arquivo JSON** (`pacientes.json`) não existir, será criado vazio.
- Caso o arquivo esteja **corrompido** ou vazio, o programa automaticamente inicia com **dados vazios**.
- Ajuste **validações** conforme necessidade (por exemplo, limitar faixas de idade, impedir nomes vazios etc.).

---
