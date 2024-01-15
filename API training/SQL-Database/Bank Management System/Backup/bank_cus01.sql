-- MySQL dump 10.13  Distrib 8.0.27, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: bank
-- ------------------------------------------------------
-- Server version	8.0.27

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `cus01`
--

DROP TABLE IF EXISTS `cus01`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cus01` (
  `S01F01` int NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `S01F02` varchar(20) NOT NULL COMMENT 'First Name',
  `S01F03` varchar(20) NOT NULL COMMENT 'Last Name',
  `S01F04` bigint NOT NULL COMMENT 'Phone',
  `S01F05` varchar(50) DEFAULT NULL COMMENT 'Address',
  PRIMARY KEY (`S01F01`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cus01`
--

LOCK TABLES `cus01` WRITE;
/*!40000 ALTER TABLE `cus01` DISABLE KEYS */;
INSERT INTO `cus01` VALUES (1,'John','Doe',7046777357,'123 Main St'),(2,'Alice','Smith',9876543210,'456 Oak St'),(3,'Bob','Johnson',5555555555,'789 Pine St'),(4,'Eva','Williams',9998887777,'101 Maple Ave'),(5,'Michael','Davis',1112223333,'202 Elm St'),(6,'Sophia','Moore',4445556666,'303 Walnut Ave'),(7,'Daniel','Taylor',7778889999,'404 Cedar St'),(8,'Olivia','Brown',2223334444,'505 Birch Ave'),(9,'James','Wilson',8889990000,'606 Oak St'),(10,'Emma','Evans',6667778888,'707 Pine St'),(11,'William','Miller',3334445555,'808 Maple Ave'),(12,'Ava','Anderson',5556667777,'909 Elm St'),(13,'Logan','Moore',1112233445,'1010 Cedar St'),(14,'Sophia','Hall',4445566778,'1111 Walnut Ave'),(15,'Lucas','Johnson',9990001111,'1212 Oak St'),(16,'Emma','White',2223334444,'1313 Pine St'),(17,'Mia','Martin',7778889999,'1414 Maple Ave'),(18,'Liam','Taylor',8889990000,'1515 Elm St'),(19,'Ava','Davis',5556667777,'1616 Cedar St'),(20,'Ethan','Smith',1112233445,'1717 Walnut Ave'),(21,'John','Doe',1234567890,'123 Main St'),(22,'Alice','Smith',9876543210,'456 Oak St'),(23,'Bob','Johnson',5555555555,'789 Pine St'),(24,'Eva','Williams',9998887777,'101 Maple Ave'),(25,'Michael','Davis',1112223333,'202 Elm St'),(26,'Sophia','Moore',4445556666,'303 Walnut Ave'),(27,'Daniel','Taylor',7778889999,'404 Cedar St'),(28,'Olivia','Brown',2223334444,'505 Birch Ave'),(29,'James','Wilson',8889990000,'606 Oak St'),(30,'Emma','Evans',6667778888,'707 Pine St'),(31,'William','Miller',3334445555,'808 Maple Ave'),(32,'Ava','Anderson',5556667777,'909 Elm St'),(33,'Logan','Moore',1112233445,'1010 Cedar St'),(34,'Sophia','Hall',4445566778,'1111 Walnut Ave'),(35,'Lucas','Johnson',9990001111,'1212 Oak St'),(36,'Emma','White',2223334444,'1313 Pine St'),(37,'Mia','Martin',7778889999,'1414 Maple Ave'),(38,'Liam','Taylor',8889990000,'1515 Elm St'),(39,'Ava','Davis',5556667777,'1616 Cedar St'),(40,'Ethan','Smith',1112233445,'1717 Walnut Ave');
/*!40000 ALTER TABLE `cus01` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-15 10:52:58
