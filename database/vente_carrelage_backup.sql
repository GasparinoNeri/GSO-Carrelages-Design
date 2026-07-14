-- MySQL dump 10.13  Distrib 9.5.0, for macos14.8 (x86_64)
--
-- Host: localhost    Database: vente_carrelage
-- ------------------------------------------------------
-- Server version	9.5.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
SET @MYSQLDUMP_TEMP_LOG_BIN = @@SESSION.SQL_LOG_BIN;
SET @@SESSION.SQL_LOG_BIN= 0;

--
-- GTID state at the beginning of the backup 
--

SET @@GLOBAL.GTID_PURGED=/*!80000 '+'*/ 'e7f9f020-d45a-11f0-add5-8d8f291d1926:1-56';

--
-- Table structure for table `adresses`
--

DROP TABLE IF EXISTS `adresses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adresses` (
  `id_adresse` bigint NOT NULL AUTO_INCREMENT,
  `id_client` bigint NOT NULL,
  `id_localite` bigint NOT NULL,
  `rue` varchar(200) NOT NULL,
  `complement` varchar(200) DEFAULT NULL,
  `type` enum('facturation','livraison') NOT NULL,
  `contact_nom` varchar(120) DEFAULT NULL,
  `contact_tel` varchar(32) DEFAULT NULL,
  PRIMARY KEY (`id_adresse`),
  KEY `fk_adresses_client` (`id_client`),
  KEY `fk_adresses_localite` (`id_localite`),
  CONSTRAINT `fk_adresses_client` FOREIGN KEY (`id_client`) REFERENCES `clients` (`id_client`) ON DELETE CASCADE,
  CONSTRAINT `fk_adresses_localite` FOREIGN KEY (`id_localite`) REFERENCES `localite` (`id_localite`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adresses`
--

LOCK TABLES `adresses` WRITE;
/*!40000 ALTER TABLE `adresses` DISABLE KEYS */;
/*!40000 ALTER TABLE `adresses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `avis_clients`
--

DROP TABLE IF EXISTS `avis_clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `avis_clients` (
  `id_avis` bigint NOT NULL AUTO_INCREMENT,
  `id_produit` bigint NOT NULL,
  `id_client` bigint DEFAULT NULL,
  `note` tinyint NOT NULL,
  `commentaire` text,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `is_moderated` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id_avis`),
  KEY `fk_avis_produit` (`id_produit`),
  KEY `fk_avis_client` (`id_client`),
  CONSTRAINT `fk_avis_client` FOREIGN KEY (`id_client`) REFERENCES `clients` (`id_client`) ON DELETE SET NULL,
  CONSTRAINT `fk_avis_produit` FOREIGN KEY (`id_produit`) REFERENCES `produits` (`id_produit`) ON DELETE CASCADE,
  CONSTRAINT `chk_note` CHECK ((`note` between 1 and 5))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `avis_clients`
--

LOCK TABLES `avis_clients` WRITE;
/*!40000 ALTER TABLE `avis_clients` DISABLE KEYS */;
/*!40000 ALTER TABLE `avis_clients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categories` (
  `id_categorie` bigint NOT NULL AUTO_INCREMENT,
  `nom` varchar(120) NOT NULL,
  `description` text,
  `id_parent` bigint DEFAULT NULL,
  PRIMARY KEY (`id_categorie`),
  KEY `fk_categories_parent` (`id_parent`),
  CONSTRAINT `fk_categories_parent` FOREIGN KEY (`id_parent`) REFERENCES `categories` (`id_categorie`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

LOCK TABLES `categories` WRITE;
/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` VALUES (1,'Carrelage sol','Carrelage destiné aux sols',NULL),(2,'Carrelage mural','Carrelage destiné aux murs',NULL),(3,'Mosaïque','Petits carreaux décoratifs',NULL);
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clients`
--

DROP TABLE IF EXISTS `clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clients` (
  `id_client` bigint NOT NULL AUTO_INCREMENT,
  `nom` varchar(120) NOT NULL,
  `prenom` varchar(120) DEFAULT NULL,
  `email` varchar(190) DEFAULT NULL,
  `tel` varchar(32) DEFAULT NULL,
  `date_inscription` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `statut` enum('actif','inactif') NOT NULL DEFAULT 'actif',
  PRIMARY KEY (`id_client`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clients`
--

LOCK TABLES `clients` WRITE;
/*!40000 ALTER TABLE `clients` DISABLE KEYS */;
/*!40000 ALTER TABLE `clients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `commandes`
--

DROP TABLE IF EXISTS `commandes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `commandes` (
  `id_commande` bigint NOT NULL AUTO_INCREMENT,
  `id_client` bigint NOT NULL,
  `id_adresse_facturation` bigint NOT NULL,
  `id_adresse_livraison` bigint NOT NULL,
  `statut` enum('en_attente','payee','expediee','livree','annulee') NOT NULL DEFAULT 'en_attente',
  `total_ttc` decimal(12,2) NOT NULL DEFAULT '0.00',
  `devise` char(3) NOT NULL DEFAULT 'EUR',
  `lignes_json` text,
  `payment_status` enum('none','pending','paid','failed','refunded','partial_refund') NOT NULL DEFAULT 'none',
  `payment_method` varchar(32) DEFAULT NULL,
  `payment_amount` decimal(12,2) NOT NULL DEFAULT '0.00',
  `payment_tx_ref` varchar(120) DEFAULT NULL,
  `paid_at` datetime DEFAULT NULL,
  `refunded_at` datetime DEFAULT NULL,
  `refund_amount` decimal(12,2) NOT NULL DEFAULT '0.00',
  `payment_meta` text,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id_commande`),
  KEY `fk_commandes_client` (`id_client`),
  KEY `fk_cmd_addr_fact` (`id_adresse_facturation`),
  KEY `fk_cmd_addr_livr` (`id_adresse_livraison`),
  CONSTRAINT `fk_cmd_addr_fact` FOREIGN KEY (`id_adresse_facturation`) REFERENCES `adresses` (`id_adresse`) ON DELETE RESTRICT,
  CONSTRAINT `fk_cmd_addr_livr` FOREIGN KEY (`id_adresse_livraison`) REFERENCES `adresses` (`id_adresse`) ON DELETE RESTRICT,
  CONSTRAINT `fk_commandes_client` FOREIGN KEY (`id_client`) REFERENCES `clients` (`id_client`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commandes`
--

LOCK TABLES `commandes` WRITE;
/*!40000 ALTER TABLE `commandes` DISABLE KEYS */;
/*!40000 ALTER TABLE `commandes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fournisseurs`
--

DROP TABLE IF EXISTS `fournisseurs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fournisseurs` (
  `id_fournisseur` bigint NOT NULL AUTO_INCREMENT,
  `raison_sociale` varchar(160) NOT NULL,
  `contact_nom` varchar(120) DEFAULT NULL,
  `email` varchar(190) DEFAULT NULL,
  `tel` varchar(32) DEFAULT NULL,
  `adresse_txt` varchar(255) DEFAULT NULL,
  `delai_reappro_j` int DEFAULT NULL,
  `conditions_paiement` varchar(120) DEFAULT NULL,
  `url_catalogue` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_fournisseur`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fournisseurs`
--

LOCK TABLES `fournisseurs` WRITE;
/*!40000 ALTER TABLE `fournisseurs` DISABLE KEYS */;
INSERT INTO `fournisseurs` VALUES (1,'Carrelage Pro','Marc Dupont','marc@carrelagepro.test','+32000000000',NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `fournisseurs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `livraisons`
--

DROP TABLE IF EXISTS `livraisons`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `livraisons` (
  `id_livraison` bigint NOT NULL AUTO_INCREMENT,
  `id_commande` bigint NOT NULL,
  `transporteur` varchar(60) DEFAULT NULL,
  `tracking` varchar(120) DEFAULT NULL,
  `date_expedition` date DEFAULT NULL,
  `date_livraison_prevue` date DEFAULT NULL,
  `date_livraison_effective` date DEFAULT NULL,
  `statut` enum('preparee','en_transit','livree','retour') DEFAULT 'preparee',
  PRIMARY KEY (`id_livraison`),
  KEY `fk_livraisons_commande` (`id_commande`),
  CONSTRAINT `fk_livraisons_commande` FOREIGN KEY (`id_commande`) REFERENCES `commandes` (`id_commande`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `livraisons`
--

LOCK TABLES `livraisons` WRITE;
/*!40000 ALTER TABLE `livraisons` DISABLE KEYS */;
/*!40000 ALTER TABLE `livraisons` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `localite`
--

DROP TABLE IF EXISTS `localite`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `localite` (
  `id_localite` bigint NOT NULL AUTO_INCREMENT,
  `localite` char(32) NOT NULL,
  `code_postal` varchar(16) NOT NULL,
  `zone_tarif_livraison` varchar(32) DEFAULT NULL,
  PRIMARY KEY (`id_localite`),
  UNIQUE KEY `uq_localite` (`localite`,`code_postal`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `localite`
--

LOCK TABLES `localite` WRITE;
/*!40000 ALTER TABLE `localite` DISABLE KEYS */;
/*!40000 ALTER TABLE `localite` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `produits`
--

DROP TABLE IF EXISTS `produits`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `produits` (
  `id_produit` bigint NOT NULL AUTO_INCREMENT,
  `nom` varchar(160) NOT NULL,
  `description` text,
  `id_categorie` bigint NOT NULL,
  `id_fournisseur` bigint DEFAULT NULL,
  `prix_unitaire` decimal(12,2) NOT NULL DEFAULT '0.00',
  `stock_on_hand` int NOT NULL DEFAULT '0',
  `stock_reserved` int NOT NULL DEFAULT '0',
  `longueur_mm` int DEFAULT NULL,
  `largeur_mm` int DEFAULT NULL,
  `epaisseur_mm` int DEFAULT NULL,
  `poids_m2` decimal(10,3) DEFAULT NULL,
  `actif` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_produit`),
  KEY `fk_produits_categorie` (`id_categorie`),
  KEY `fk_produits_fournisseur` (`id_fournisseur`),
  CONSTRAINT `fk_produits_categorie` FOREIGN KEY (`id_categorie`) REFERENCES `categories` (`id_categorie`) ON DELETE RESTRICT,
  CONSTRAINT `fk_produits_fournisseur` FOREIGN KEY (`id_fournisseur`) REFERENCES `fournisseurs` (`id_fournisseur`) ON DELETE SET NULL,
  CONSTRAINT `chk_stock_disp` CHECK ((`stock_on_hand` >= `stock_reserved`)),
  CONSTRAINT `chk_stock_nonneg` CHECK (((`stock_on_hand` >= 0) and (`stock_reserved` >= 0)))
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produits`
--

LOCK TABLES `produits` WRITE;
/*!40000 ALTER TABLE `produits` DISABLE KEYS */;
INSERT INTO `produits` VALUES (4,'Carrelage marbre blanc ','Carrelage effet marbre pour intérieur',1,1,29.90,50,0,600,600,10,22.500,1),(5,'Carrelage béton gris','Carrelage moderne effet béton',1,1,24.90,80,0,600,600,9,21.000,1),(6,'Mosaïque salle de bain','Mosaïque adaptée aux murs de salle de bain',3,1,19.90,100,0,300,300,8,18.000,1),(7,'Carrelage en marbre','Marbre turque',1,1,23.00,16,0,NULL,NULL,NULL,NULL,1),(8,'','',1,1,0.00,0,0,NULL,NULL,NULL,NULL,1),(9,'','',1,1,0.00,0,0,NULL,NULL,NULL,NULL,1),(10,'','',1,1,0.00,0,0,NULL,NULL,NULL,NULL,1),(11,'','',1,1,0.00,0,0,NULL,NULL,NULL,NULL,1),(12,'','',1,1,0.00,0,0,NULL,NULL,NULL,NULL,1),(13,'','',1,1,0.00,0,0,NULL,NULL,NULL,NULL,1),(14,'','',1,1,0.00,0,0,NULL,NULL,NULL,NULL,1),(15,'','',1,1,0.00,0,0,NULL,NULL,NULL,NULL,1),(16,'','',1,1,0.00,0,0,NULL,NULL,NULL,NULL,1),(17,'','',1,1,0.00,0,0,NULL,NULL,NULL,NULL,1),(18,'','',1,1,0.00,0,0,NULL,NULL,NULL,NULL,1),(19,'','',1,1,0.00,0,0,NULL,NULL,NULL,NULL,1),(20,'','',1,1,0.00,0,0,NULL,NULL,NULL,NULL,1),(21,'','',1,1,0.00,0,0,NULL,NULL,NULL,NULL,1);
/*!40000 ALTER TABLE `produits` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `utilisateurs`
--

DROP TABLE IF EXISTS `utilisateurs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `utilisateurs` (
  `id_utilisateur` bigint NOT NULL AUTO_INCREMENT,
  `nom` varchar(120) NOT NULL,
  `prenom` varchar(120) DEFAULT NULL,
  `email` varchar(190) NOT NULL,
  `mot_de_passe` varchar(255) NOT NULL,
  `role` varchar(20) NOT NULL DEFAULT 'client',
  `actif` tinyint NOT NULL DEFAULT '1',
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id_utilisateur`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `utilisateurs`
--

LOCK TABLES `utilisateurs` WRITE;
/*!40000 ALTER TABLE `utilisateurs` DISABLE KEYS */;
INSERT INTO `utilisateurs` VALUES (1,'Neri','Gasparino','admin@gso.be','admin123','admin',1,'2026-06-15 22:01:19');
/*!40000 ALTER TABLE `utilisateurs` ENABLE KEYS */;
UNLOCK TABLES;
SET @@SESSION.SQL_LOG_BIN = @MYSQLDUMP_TEMP_LOG_BIN;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-06-17 22:03:12
