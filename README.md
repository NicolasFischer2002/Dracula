# 🧛‍♂️ Drácula — Sistema de Pedidos (Full-Stack)

## 📌 Visão Geral

**Drácula** é um projeto **full-stack** de um sistema de pedidos para estabelecimentos como  
**restaurantes, lanchonetes, pizzarias e similares**.

Atualmente, o desenvolvimento está **concentrado no back-end**, com foco total na **modelagem
do domínio, arquitetura e regras de negócio**. As camadas de front-end serão abordadas em
uma fase posterior do projeto.

O projeto foi criado com um propósito **educacional e arquitetural**, funcionando como um  
**ambiente realista de estudo**, onde conceitos avançados de engenharia de software são  
aplicados de forma prática, incremental e consciente.

O Drácula é **aberto para estudo e colaboração**, porém **restrito a uso não comercial**.

---

## 🎯 Objetivos do Projeto

O principal objetivo do Drácula é servir como um **laboratório de aprendizado avançado**, aplicando:

- C# e .NET
- Programação Orientada a Objetos (POO)
- Domain-Driven Design (DDD)
- Clean Architecture
- Clean Code
- Design Patterns
- Boas práticas de modelagem e código
- Testes unitários e de integração

Além disso, o projeto busca:

- Criar um **núcleo de domínio forte e expressivo**
- Explorar decisões arquiteturais reais
- Compartilhar conhecimento com a comunidade
- Evoluir continuamente com base em boas práticas

---

## ✨ Principais Características

- 🧠 Modelagem rica de domínio baseada em **DDD**
- 🧩 Organização clara por **Bounded Contexts**
- 🏗️ Arquitetura limpa e modular
- 🛡️ Invariantes de domínio protegidas no core
- 💰 Value Objects robustos (`Money`, `Currency`, etc.)
- ⏱️ Datas e horários padronizados em **UTC**
- 🚫 Evita `null` sempre que possível (Null Object Pattern)
- 🧪 Forte incentivo a testes automatizados
- ♻️ Código focado em legibilidade, coesão e baixo acoplamento

---

## 🗂️ Escopo Inicial

### 🍽️ Menu (Cardápio)
- Gerenciamento dos items do menu

### 🛒 Ordering (Pedidos)
- Gerenciamento dos pedidos

### 📦 Evolução Futura
- Desenvolvimento continuado do back-end
- Bancos de dados da aplicação
- APIs para acesso aos serviços
- Front-end da aplicação
- Integrações

---

## 🏛️ Arquitetura

O Drácula segue os princípios de **Clean Architecture** combinados com  
**Domain-Driven Design (DDD)**.

A arquitetura é organizada por **Bounded Contexts**, cada um contendo suas próprias camadas
e responsabilidades bem definidas, com foco em:

- Isolamento de regras de negócio
- Independência de frameworks
- Clareza de intenção
- Evolução sustentável do código

---

## 🤝 Contribuição

O Drácula é um projeto **educacional e colaborativo**.

### Como contribuir

1. Abra uma **issue** para discussão
2. Crie um **fork** do repositório
3. Desenvolva em uma **branch descritiva**
4. Envie um **Pull Request** com testes e explicações

### Diretrizes

- PRs pequenos e focados
- Código limpo e testável
- Respeitar as decisões arquiteturais
- Priorizar clareza e intenção do código

---

## ⚠️ Licença e Uso

Este projeto é **aberto apenas para fins educacionais e não comerciais**.

### ✔️ Permitido
- Estudo
- Aprendizado
- Forks educacionais
- Contribuições

### ❌ Não permitido
- Uso comercial
- Distribuição com fins lucrativos
- Uso em produtos pagos

---

## 🗺️ Roadmap

- Consolidar modelagem dos Aggregates
- APIs REST
- Início do desenvolvimento do front-end
- Persistência com EF Core

---

## 👤 Autor

**Nicolas Fischer**  
Projeto criado com foco em aprendizado profundo, arquitetura e compartilhamento de conhecimento.

---

## 🙏 Agradecimentos

Inspirado por:

- Eric Evans — *Domain-Driven Design*
- Robert C. Martin — *Clean Code & Clean Architecture*
