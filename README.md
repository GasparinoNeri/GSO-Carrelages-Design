# GSO Carrelages & Design

Projet réalisé dans le cadre du cours **Projet de développement web**.

## 1. Présentation

**GSO Carrelages & Design** est une application web de vente et de gestion de carrelages.

L'application permet à un visiteur ou à un client de consulter les produits, de créer un compte, de gérer son profil, d'ajouter des produits à son panier et de passer une commande.

Un espace administrateur permet également de gérer les produits ainsi que les commandes des clients.

Le projet est composé de :

* un frontend développé avec Angular ;
* une API développée avec ASP.NET Core ;
* une base de données MySQL ;
* Dapper pour l'accès aux données.

Le backend suit la **Clean Architecture vue au cours** avec une séparation claire entre :

* API ;
* Core ;
* Infrastructure.

Entity Framework n'est pas utilisé.

---

# 2. Technologies utilisées

## Frontend

* Angular 21
* Angular CLI 21.2.7
* TypeScript 5.9
* HTML
* CSS
* RxJS
* Angular Forms
* Angular Router
* HttpClient
* Angular Signals

La nouvelle syntaxe Angular est utilisée dans le projet :

```text
@if
@for
```

La gestion de l'état est réalisée uniquement via des **Services Angular**.

Aucune bibliothèque externe de gestion d'état telle que NgRx ou Redux n'est utilisée.

## Backend

* ASP.NET Core Web API
* .NET 10
* C#
* Dapper
* BCrypt.Net

## Base de données

* MySQL 9.5.0

---

# 3. Versions utilisées durant le développement

Les versions utilisées sur la machine de développement sont :

```text
.NET SDK : 10.0.301
Angular : 21.2
Angular CLI : 21.2.7
Node.js : 25.8.0
npm : 11.11.0
MySQL : 9.5.0
TypeScript : 5.9
```

---

# 4. Fonctionnalités principales

Le projet comporte plus de cinq fonctionnalités distinctes.

## 4.1 Authentification

* création d'un compte client ;
* connexion ;
* déconnexion ;
* gestion des rôles client et administrateur ;
* stockage sécurisé des nouveaux mots de passe avec BCrypt.

## 4.2 Gestion du profil

Un utilisateur connecté peut :

* consulter ses informations ;
* modifier son nom ;
* modifier son prénom ;
* modifier son téléphone ;
* modifier son adresse ;
* modifier sa date de naissance ;
* ajouter une URL de photo de profil.

## 4.3 Catalogue de produits

L'utilisateur peut :

* consulter le catalogue ;
* consulter les informations d'un produit ;
* consulter le prix ;
* consulter le stock disponible ;
* accéder à la fiche détaillée d'un produit.

## 4.4 Gestion des produits par l'administrateur

L'administrateur dispose d'un CRUD complet sur les produits :

* création ;
* consultation ;
* modification ;
* suppression.

Le CRUD complet est considéré comme une seule fonctionnalité.

## 4.5 Panier

Le client peut :

* ajouter un produit au panier ;
* supprimer un produit ;
* gérer plusieurs produits ;
* consulter les quantités ;
* consulter le sous-total ;
* consulter le total du panier ;
* vider le panier.

L'état du panier est géré par un Service Angular.

## 4.6 Création d'une commande

Un client connecté peut passer une commande.

Lors de la création d'une commande, l'application gère :

* le client ;
* la localité ;
* l'adresse de facturation ;
* l'adresse de livraison ;
* les lignes de commande ;
* le total TTC ;
* le statut initial ;
* la devise.

La création d'une commande utilise une transaction SQL.

Si toutes les opérations réussissent :

```text
COMMIT
```

Si une erreur survient :

```text
ROLLBACK
```

Cela permet de conserver la cohérence des données.

## 4.7 Historique des commandes

Le client peut consulter depuis son profil :

* ses commandes ;
* les produits commandés ;
* les quantités ;
* le montant total ;
* le statut de chaque commande.

## 4.8 Gestion des commandes par l'administrateur

L'administrateur peut :

* consulter toutes les commandes ;
* voir le client concerné ;
* consulter les produits commandés ;
* consulter le montant total ;
* modifier le statut d'une commande.

Les statuts disponibles sont :

```text
en_attente
payee
expediee
livree
annulee
```

---

# 5. Architecture générale

Le projet est organisé de la manière suivante :

```text
GSO-Carrelages-Design
│
├── frontend-angular
│
├── backend-dotnet
│
├── database
│   └── vente_carrelage.sql
│
└── README.md
```

---

# 6. Architecture Backend

Le backend respecte la structure demandée :

```text
backend-dotnet
│
├── GsoCarrelages.Api
│
├── GsoCarrelages.Core
│
└── GsoCarrelages.Infrastructure
```

## GsoCarrelages.Api

Cette couche contient principalement :

* les Controllers ;
* la configuration de l'application ;
* la configuration CORS ;
* le point d'entrée `Program.cs`.

Les Controllers ne communiquent jamais directement avec la base de données.

## GsoCarrelages.Core

Cette couche contient :

* les entités métier ;
* les interfaces des UseCases ;
* les UseCases ;
* les interfaces des Gateways.

Exemples :

```text
Entities
UseCases
UseCases/Abstractions
IGateways
```

Le Core ne dépend pas de Dapper ou de MySQL.

## GsoCarrelages.Infrastructure

Cette couche contient :

* les Models Infrastructure ;
* les Gateways ;
* les Repositories ;
* les interfaces des Repositories ;
* la connexion MySQL ;
* les requêtes SQL ;
* Dapper.

---

# 7. Flux applicatif

Le flux général d'une donnée est :

```text
Angular Component
        ↓
Angular Service
        ↓
HttpClient
        ↓
API Controller
        ↓
UseCase
        ↓
Gateway
        ↓
Repository
        ↓
Dapper
        ↓
MySQL
```

La donnée revient ensuite dans le sens inverse jusqu'à l'interface Angular.

Exemple pour les produits :

```text
Catalog Component
        ↓
ProductService
        ↓
ProductsController
        ↓
ProductUseCases
        ↓
ProductGateway
        ↓
ProductRepository
        ↓
Dapper
        ↓
table produits
```

---

# 8. Prérequis

Avant de lancer le projet, installer :

* .NET SDK 10 ;
* Node.js ;
* npm ;
* MySQL.

Vérification des versions :

```bash
dotnet --version
```

```bash
node --version
```

```bash
npm --version
```

```bash
mysql --version
```

---

# 9. Installation du projet

Cloner le dépôt GitHub ou extraire l'archive ZIP.

Se placer ensuite à la racine :

```bash
cd GSO-Carrelages-Design
```

---

# 10. Création de la base de données

Le script SQL nécessaire est présent dans :

```text
database/vente_carrelage.sql
```

Se connecter à MySQL :

```bash
mysql -u root -p
```

Créer la base :

```sql
CREATE DATABASE vente_carrelage;
```

Puis quitter MySQL :

```sql
exit;
```

---

# 11. Import du script SQL

Depuis la racine du projet :

## macOS / Linux

```bash
mysql -u root -p vente_carrelage < database/vente_carrelage.sql
```

## Windows PowerShell

```powershell
Get-Content database\vente_carrelage.sql | mysql -u root -p vente_carrelage
```

Le script contient :

* les tables ;
* les clés primaires ;
* les clés étrangères ;
* les contraintes ;
* les produits de démonstration ;
* les catégories ;
* les fournisseurs ;
* les comptes de démonstration ;
* une commande de démonstration.

---

# 12. Configuration MySQL du Backend

La chaîne de connexion se trouve dans :

```text
backend-dotnet/GsoCarrelages.Api/appsettings.json
```

Configuration actuelle :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=vente_carrelage;Uid=root;"
  }
}
```

Cette configuration correspond à un utilisateur MySQL `root` sans mot de passe.

Si l'utilisateur MySQL possède un mot de passe, modifier la chaîne de connexion :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=vente_carrelage;Uid=root;Pwd=VOTRE_MOT_DE_PASSE;"
  }
}
```

---

# 13. Lancement du Backend

Depuis la racine :

```bash
cd backend-dotnet
```

Restaurer les packages :

```bash
dotnet restore
```

Compiler :

```bash
dotnet build
```

Lancer l'API :

```bash
dotnet run --project GsoCarrelages.Api
```

Dans la configuration de développement utilisée pour ce projet, l'API est accessible sur :

```text
http://localhost:5071
```

Test possible :

```text
http://localhost:5071/api/Products
```

Cette URL doit retourner les produits au format JSON.

Il faut laisser ce terminal ouvert pendant l'utilisation de l'application.

---

# 14. Lancement du Frontend

Ouvrir un deuxième terminal.

Depuis la racine du projet :

```bash
cd frontend-angular
```

Installer les dépendances :

```bash
npm install
```

Lancer Angular :

```bash
npm start
```

La commande `npm start` exécute :

```text
ng serve
```

L'application est ensuite accessible sur :

```text
http://localhost:4200
```

---

# 15. Comptes de démonstration

Deux comptes sont fournis dans le script SQL.

## Administrateur

```text
Email : admin.demo@gso.test
Mot de passe : Admin1234!
```

Ce compte permet notamment :

* d'accéder à l'espace administrateur ;
* de créer des produits ;
* de modifier des produits ;
* de supprimer des produits ;
* de consulter les commandes ;
* de modifier le statut des commandes.

## Client

```text
Email : client.demo@gso.test
Mot de passe : Client1234!
```

Ce compte permet notamment :

* d'accéder au catalogue ;
* d'utiliser le panier ;
* de passer une commande ;
* de consulter son profil ;
* de modifier son profil ;
* de consulter l'historique de ses commandes.

Une commande de démonstration est déjà associée au compte client.

---

# 16. Vérification du fonctionnement

Après le lancement du backend et du frontend :

## Test du catalogue

Ouvrir :

```text
http://localhost:4200
```

Puis accéder à la page Produits.

Les produits stockés dans MySQL doivent apparaître.

## Test client

Se connecter avec :

```text
client.demo@gso.test
Client1234!
```

Vérifier :

1. l'accès au profil ;
2. l'historique des commandes ;
3. le catalogue ;
4. l'ajout au panier ;
5. la création d'une commande.

## Test administrateur

Se connecter avec :

```text
admin.demo@gso.test
Admin1234!
```

Vérifier :

1. l'accès à l'administration ;
2. l'affichage des produits ;
3. la création d'un produit ;
4. la modification d'un produit ;
5. la suppression d'un produit ;
6. l'affichage des commandes clients ;
7. la modification du statut d'une commande.

---

# 17. Communication Angular / API

Les appels vers l'API sont réalisés avec :

```text
HttpClient
```

via des Services Angular dédiés.

Exemples :

```text
AuthService
ProductService
OrderService
CartService
```

Les Components Angular ne communiquent pas directement avec MySQL.

---

# 18. Gestion de l'état Angular

La gestion de l'état est réalisée exclusivement via les Services Angular.

Le projet utilise notamment des Signals Angular pour conserver certaines données en mémoire.

Exemples :

```text
currentUser
products
items du panier
orders
```

Aucune bibliothèque externe telle que :

```text
NgRx
Redux
```

n'est utilisée.

---

# 19. Accès aux données

L'accès aux données est effectué exclusivement avec :

```text
Dapper
```

Les requêtes SQL se trouvent dans les Repositories de la couche Infrastructure.

Aucun Controller ne communique directement avec MySQL.

Entity Framework n'est pas utilisé.

---

# 20. Sécurité des mots de passe

Les nouveaux mots de passe utilisateurs sont hashés avec :

```text
BCrypt
```

Lors de la connexion, le mot de passe saisi est vérifié par rapport au hash enregistré en base de données.

---

# 21. Structure importante du Backend

Exemple pour les produits :

```text
ProductsController
        ↓
IProductUseCases
        ↓
ProductUseCases
        ↓
IProductGateway
        ↓
ProductGateway
        ↓
IProductRepository
        ↓
ProductRepository
        ↓
Dapper
        ↓
MySQL
```

Exemple pour les commandes :

```text
OrdersController
        ↓
IOrderUseCases
        ↓
OrderUseCases
        ↓
IOrderGateway
        ↓
OrderGateway
        ↓
IOrderRepository
        ↓
OrderRepository
        ↓
Dapper
        ↓
MySQL
```

---

# 22. Compilation

## Frontend

Depuis :

```text
frontend-angular
```

exécuter :

```bash
npm run build
```

## Backend

Depuis :

```text
backend-dotnet
```

exécuter :

```bash
dotnet build
```

---

# 23. Dossiers générés

Les dossiers générés automatiquement ne doivent pas être ajoutés à l'archive ZIP ou au dépôt.

Notamment :

```text
node_modules
bin
obj
```

Ils sont exclus du dépôt via `.gitignore`.

---

# 24. Auteur

**Gasparino Neri**

Projet réalisé dans le cadre du cours **Projet de développement web**.
