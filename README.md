# GSO Carrelages & Design

Projet réalisé dans le cadre du cours **Projet de développement web**.

## Présentation

GSO Carrelages & Design est une application web permettant la consultation et la gestion d'un catalogue de carrelages.

Le projet est développé selon une architecture séparée :

* Frontend Angular
* Backend ASP.NET Core Web API
* Base de données MySQL

Le backend respecte les principes de la **Clean Architecture** vus au cours :

* Séparation des responsabilités
* Architecture en couches
* Utilisation exclusive de Dapper pour l'accès aux données
* Respect des principes SOLID

---

## Technologies utilisées

### Frontend

* Angular 21
* TypeScript
* HTML
* CSS

### Backend

* ASP.NET Core Web API (.NET 10)
* C#
* Dapper

### Base de données

* MySQL

---

## Fonctionnalités

### Visiteur

* Consultation du catalogue
* Consultation du détail d'un produit
* Ajout de produits au panier
* Suppression de produits du panier
* Calcul automatique du total du panier

### Administrateur

* Connexion administrateur
* Consultation des produits
* Ajout d'un produit
* Modification d'un produit
* Suppression d'un produit
* Consultation du profil administrateur

---

## Architecture du projet

```text
GSO-Carrelages-Design
├── frontend-angular
├── backend-dotnet
├── database
└── README.md
```

### Architecture Backend

```text
backend-dotnet
├── GsoCarrelages.Api
├── GsoCarrelages.Core
└── GsoCarrelages.Infrastructure
```

#### GsoCarrelages.Api

* Contrôleurs API
* Configuration de l'application
* Point d'entrée du projet

#### GsoCarrelages.Core

* Entités métier
* Interfaces

#### GsoCarrelages.Infrastructure

* Accès aux données
* Repositories Dapper
* Gestion des connexions MySQL

---

## Base de données

Le script SQL fourni se trouve dans :

```text
database/vente_carrelage.sql
```

Nom de la base de données :

```text
vente_carrelage
```

---

## Données de démonstration

Après import du script SQL, la base contient :

* 4 produits de démonstration
* 3 catégories
* 1 fournisseur
* 1 compte administrateur

Compte administrateur :

```text
Email : admin@gso.be
Mot de passe : admin123
```

---

## Prérequis

Avant de lancer le projet, installer :

* .NET SDK 10
* Node.js
* npm
* MySQL

Versions utilisées durant le développement :

* .NET SDK 10.0.301
* Angular CLI 21.2.7
* Node.js 25.8.0
* MySQL 9.5.0

---

## Installation de la base de données

Créer la base :

```sql
CREATE DATABASE vente_carrelage;
```

### Import du script SQL

#### macOS / Linux

```bash
mysql -u root -p vente_carrelage < database/vente_carrelage.sql
```

#### Windows PowerShell

```powershell
Get-Content database\vente_carrelage.sql | mysql -u root -p vente_carrelage
```

Le script crée automatiquement :

* Les tables
* Les contraintes
* Les données de démonstration
* Le compte administrateur

---

## Configuration de la connexion MySQL

La chaîne de connexion se trouve dans :

```text
backend-dotnet/GsoCarrelages.Api/appsettings.json
```

Configuration par défaut :

```json
"DefaultConnection": "Server=localhost;Port=3306;Database=vente_carrelage;Uid=root;"
```

Si votre compte MySQL possède un mot de passe :

```json
"DefaultConnection": "Server=localhost;Port=3306;Database=vente_carrelage;Uid=root;Pwd=VOTRE_MOT_DE_PASSE;"
```

Exemple :

```json
"DefaultConnection": "Server=localhost;Port=3306;Database=vente_carrelage;Uid=root;Pwd=Police21;"
```

---

## Lancement du backend

Depuis la racine du projet :

```bash
cd backend-dotnet
```

Restaurer les dépendances :

```bash
dotnet restore
```

Compiler le projet :

```bash
dotnet build
```

Démarrer l'API :

```bash
dotnet run --project GsoCarrelages.Api
```

L'API démarre sur :

```text
http://localhost:5071
```

---

## Lancement du frontend

Ouvrir un second terminal puis se placer dans :

```bash
cd frontend-angular
```

Installer les dépendances :

```bash
npm install
```

Sous Windows PowerShell, si les scripts sont bloqués :

```powershell
npm.cmd install
```

Démarrer Angular :

```bash
ng serve
```

ou sous Windows :

```powershell
npx.cmd ng serve
```

L'application est accessible à :

```text
http://localhost:4200
```

---

## Vérification du bon fonctionnement

### Vérification de l'API

Ouvrir :

```text
http://localhost:5071/api/Products
```

Cette URL doit retourner la liste des produits au format JSON.

### Vérification du frontend

Ouvrir :

```text
http://localhost:4200
```

Le catalogue doit afficher les produits présents dans la base de données.

### Vérification de la connexion administrateur

Utiliser les identifiants suivants :

```text
Email : admin@gso.be
Mot de passe : admin123
```

---

## Auteur

**Gasparino Neri**

Projet réalisé dans le cadre du cours **Projet de développement web**.
