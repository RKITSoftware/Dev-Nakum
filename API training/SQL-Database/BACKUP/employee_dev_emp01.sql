-- MySQL dump 10.13  Distrib 8.0.27, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: employee_dev
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
-- Table structure for table `emp01`
--

DROP TABLE IF EXISTS `emp01`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `emp01` (
  `P01F01` int NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `P01F02` varchar(50) DEFAULT NULL COMMENT 'Name',
  `P01F03` int DEFAULT NULL COMMENT 'Age',
  `P01F04` varchar(30) DEFAULT NULL COMMENT 'Designation',
  `P01F05` int DEFAULT NULL COMMENT 'Salary',
  PRIMARY KEY (`P01F01`),
  KEY `idx_P01F04` (`P01F04`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `emp01`
--

LOCK TABLES `emp01` WRITE;
/*!40000 ALTER TABLE `emp01` DISABLE KEYS */;
INSERT INTO `emp01` VALUES (1,'Dev',21,'SDE',10000),(2,'Raj',21,'SDE',30000),(3,'Tushar',22,'UI/UX',35000),(4,'Kishan',26,'Engineer',50000),(7,'John Doe',30,'SDE',75000),(8,'SUPER Alice Smith',28,'Data Analyst',60000),(9,'Bob Johnson',35,'Network Engineer',80000),(10,'Eva Brown',32,'Database Administrator',70000),(11,'John Doe',30,'SDE',75000),(12,'SUPER Alice Smith',28,'Data Analyst',60000),(13,'Bob Johnson',35,'Network Engineer',80000),(14,'Eva Brown',32,'Database Administrator',70000),(15,'Michael White',27,'SDE',72000),(16,'Sophia Davis',29,'Data Analyst',55000),(17,'Daniel Miller',33,'Network Engineer',85000),(18,'Olivia Wilson',31,'Database Administrator',68000),(19,'William Moore',26,'SDE',71000),(20,'Emma Jones',34,'Data Analyst',58000);
/*!40000 ALTER TABLE `emp01` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-15 12:42:45
