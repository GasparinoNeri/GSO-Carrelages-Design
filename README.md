# GSO Carrelages & Design

Projet réalisé dans le cadre du cours **Projet de développement web**.

Ce projet consiste à développer une application web de vente et de gestion de carrelages, en se basant sur le dossier d’analyse et de conception réalisé précédemment.

## Technologies utilisées

### Frontend

* Angular
* TypeScript
* HTML
* CSS

### Backend

* ASP.NET Core Web API
* C#
* Dapper

### Base de données

* MySQL

## Description

GSO Carrelages & Design est une application web permettant la consultation et la gestion d’un catalogue de carrelages.

Le projet utilise une architecture séparée :

* Frontend Angular
* Backend ASP.NET Core Web API
* Base de données MySQL

L’application permet à un utilisateur de consulter les produits disponibles, visualiser le détail d’un produit et gérer un panier.

Un administrateur peut également se connecter afin d’ajouter, modifier ou supprimer des produits.

## Fonctionnalités

### Visiteur

* Consultation du catalogue
* Consultation du détail d’un produit
* Ajout de produits au panier
* Suppression de produits du panier
* Calcul automatique du total du panier

### Administrateur

* Connexion administrateur
* Ajout d’un produit
* Modification d’un produit
* Suppression d’un produit
* Consultation du profil administrateur

## Architecture du projet

```text
GSO-Carrelages-Design
├── frontend-angular
├── backend-dotnet
├── database
└── README.md
```

### Frontend Angular

Pages principales :

* Accueil
* Catalogue
* Détail produit
* Panier
* Connexion
* Administration
* Profil

Les appels vers l’API sont réalisés avec `HttpClient` dans des services Angular.

### Backend .NET

Le backend respecte une architecture en couches :

```text
backend-dotnet
├── GsoCarrelages.Api
├── GsoCarrelages.Core
└── GsoCarrelages.Infrastructure
```

* `GsoCarrelages.Api` contient les contrôleurs de l’API.
* `GsoCarrelages.Core` contient les entités et interfaces.
* `GsoCarrelages.Infrastructure` contient l’accès aux données avec Dapper.

## Base de données

Le script SQL se trouve dans :

```text
database/vente_carrelage.sql
```

La base de données utilisée s’appelle :

```text
vente_carrelage
```

## Configuration de la base de données

La chaîne de connexion se trouve dans :

```text
backend-dotnet/GsoCarrelages.Api/appsettings.json
```

Configuration utilisée :

```json
"DefaultConnection": "Server=localhost;Port=3306;Database=vente_carrelage;Uid=root;"
```

## Installation et lancement

### Prérequis

* .NET SDK 10.0
* Node.js
* Angular CLI
* MySQL

### Versions utilisées

* .NET 10.0.202
* Angular CLI 21.2.7
* Node.js 25.8.0
* MySQL 9.5.0

### Importation de la base de données

Créer d'abord la base de données :

```sql
CREATE DATABASE vente_carrelage;
```

Puis importer le script SQL fourni :

```bash
mysql -u root vente_carrelage < database/vente_carrelage.sql
```

Le script crée les tables nécessaires ainsi que les données de démonstration utilisées par l'application.

### Lancement du backend

Se placer dans :

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

Démarrer l’API :

```bash
dotnet run --project GsoCarrelages.Api
```

L'API démarre sur :

```text
http://localhost:5071
```

### Lancement du frontend

Ouvrir un deuxième terminal puis se placer dans :

```bash
cd frontend-angular
```

Installer les dépendances Angular :

```bash
npm install
```

Démarrer l'application Angular :

```bash
ng serve
```

L'application est accessible à l'adresse :

```text
http://localhost:4200
```

## Compte de démonstration

Pour accéder à la partie administration :

```text
Email : ririneri.1997@gmail.com
Mot de passe : Gasparino0411
```

## Auteur

Gasparino Neri

Projet réalisé dans le cadre du cours **Projet de développement web**.
