-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: localhost    Database: myclinic_db
-- ------------------------------------------------------
-- Server version	8.0.41

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
-- Table structure for table `appointment_tbl`
--
CREATE DATABASE myclinic_db;
use myclinic_db;

DROP TABLE IF EXISTS `appointment_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `appointment_tbl` (
  `DetailID` int NOT NULL AUTO_INCREMENT,
  `PatientOperationNo` bigint NOT NULL,
  `OperationCode` varchar(10) NOT NULL,
  `DoctorID` bigint NOT NULL,
  `DateSchedule` date NOT NULL,
  `StartTime` time NOT NULL,
  `EndTime` time NOT NULL,
  `Diagnosis` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`DetailID`),
  KEY `Convert.ToInt32(result);_idx` (`OperationCode`),
  KEY `PatientOperation11_idx` (`PatientOperationNo`),
  CONSTRAINT `PatientOperation1` FOREIGN KEY (`OperationCode`) REFERENCES `operation_tbl` (`OperationCode`),
  CONSTRAINT `PatientOperation11` FOREIGN KEY (`PatientOperationNo`) REFERENCES `patientoperation_tbl` (`PatientOperationNo`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appointment_tbl`
--

LOCK TABLES `appointment_tbl` WRITE;
/*!40000 ALTER TABLE `appointment_tbl` DISABLE KEYS */;
/*!40000 ALTER TABLE `appointment_tbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clinichistory`
--

DROP TABLE IF EXISTS `clinichistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clinichistory` (
  `PatientOperationNo` bigint NOT NULL,
  `DateDischarged` date DEFAULT NULL,
  `DateAdmitted` date NOT NULL,
  PRIMARY KEY (`PatientOperationNo`),
  CONSTRAINT `ClinicHistoryFk` FOREIGN KEY (`PatientOperationNo`) REFERENCES `patientoperation_tbl` (`PatientOperationNo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clinichistory`
--

LOCK TABLES `clinichistory` WRITE;
/*!40000 ALTER TABLE `clinichistory` DISABLE KEYS */;
/*!40000 ALTER TABLE `clinichistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `doctor_tbl`
--

DROP TABLE IF EXISTS `doctor_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `doctor_tbl` (
  `DoctorID` bigint NOT NULL,
  `FirstName` varchar(45) NOT NULL,
  `MiddleName` varchar(45) NOT NULL,
  `LastName` varchar(45) NOT NULL,
  `Age` int NOT NULL,
  `PIN` varchar(45) NOT NULL,
  `DateHired` date NOT NULL,
  `Gender` varchar(45) NOT NULL,
  `Address` varchar(100) NOT NULL,
  PRIMARY KEY (`DoctorID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `doctor_tbl`
--

LOCK TABLES `doctor_tbl` WRITE;
/*!40000 ALTER TABLE `doctor_tbl` DISABLE KEYS */;
INSERT INTO `doctor_tbl` VALUES (1,'Aeyc','Aba','Doe',54,'0928','2011-11-12','Male','Roxas'),(2,'John','Diesel','Doe',24,'1234','2020-07-15','Female','Buhangin ');
/*!40000 ALTER TABLE `doctor_tbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `doctoroperation_tbl`
--

DROP TABLE IF EXISTS `doctoroperation_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `doctoroperation_tbl` (
  `OperationCode` varchar(10) DEFAULT NULL,
  `DoctorID` bigint DEFAULT NULL,
  UNIQUE KEY `unique_operation_doctor` (`OperationCode`,`DoctorID`),
  KEY `DoctorOperation1_idx` (`DoctorID`),
  KEY `DoctorOperation2_idx` (`OperationCode`),
  CONSTRAINT `DoctorOperation1` FOREIGN KEY (`DoctorID`) REFERENCES `doctor_tbl` (`DoctorID`) ON UPDATE CASCADE,
  CONSTRAINT `DoctorOperation2` FOREIGN KEY (`OperationCode`) REFERENCES `operation_tbl` (`OperationCode`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `doctoroperation_tbl`
--

LOCK TABLES `doctoroperation_tbl` WRITE;
/*!40000 ALTER TABLE `doctoroperation_tbl` DISABLE KEYS */;
INSERT INTO `doctoroperation_tbl` VALUES ('CE43',1),('CE43',2),('CK10',1),('CK10',2),('XE101',2);
/*!40000 ALTER TABLE `doctoroperation_tbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `frontdesk_tbl`
--

DROP TABLE IF EXISTS `frontdesk_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `frontdesk_tbl` (
  `FrontDeskID` bigint NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(45) DEFAULT NULL,
  `MiddleName` varchar(45) DEFAULT NULL,
  `LastName` varchar(45) DEFAULT NULL,
  `Age` int DEFAULT NULL,
  `Username` varchar(45) NOT NULL,
  `Password` varchar(45) NOT NULL,
  `Type` varchar(45) NOT NULL,
  PRIMARY KEY (`FrontDeskID`),
  UNIQUE KEY `Username_UNIQUE` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `frontdesk_tbl`
--

LOCK TABLES `frontdesk_tbl` WRITE;
/*!40000 ALTER TABLE `frontdesk_tbl` DISABLE KEYS */;
INSERT INTO `frontdesk_tbl` VALUES (1,NULL,NULL,NULL,54,'admin','admin','admin'),(2,NULL,NULL,NULL,NULL,'a','a','staf');
/*!40000 ALTER TABLE `frontdesk_tbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `frontdeskpatient_tbl`
--

DROP TABLE IF EXISTS `frontdeskpatient_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `frontdeskpatient_tbl` (
  `FrontDeskID` bigint DEFAULT NULL,
  `PatientID` bigint DEFAULT NULL,
  KEY `FrontDeskPatient1_idx` (`PatientID`),
  KEY `FrontDeskPatient2_idx` (`FrontDeskID`),
  CONSTRAINT `FrontDeskPatient1` FOREIGN KEY (`PatientID`) REFERENCES `patient_tbl` (`PatientID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FrontDeskPatient2` FOREIGN KEY (`FrontDeskID`) REFERENCES `frontdesk_tbl` (`FrontDeskID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `frontdeskpatient_tbl`
--

LOCK TABLES `frontdeskpatient_tbl` WRITE;
/*!40000 ALTER TABLE `frontdeskpatient_tbl` DISABLE KEYS */;
/*!40000 ALTER TABLE `frontdeskpatient_tbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `operation_tbl`
--

DROP TABLE IF EXISTS `operation_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `operation_tbl` (
  `OperationCode` varchar(10) NOT NULL,
  `Name` varchar(45) NOT NULL,
  `DateAdded` date NOT NULL,
  `Description` varchar(45) DEFAULT NULL,
  `FrontDeskID` bigint DEFAULT NULL,
  `Price` decimal(10,2) NOT NULL,
  `Duration` time DEFAULT NULL,
  PRIMARY KEY (`OperationCode`),
  UNIQUE KEY `Name_UNIQUE` (`Name`),
  KEY `OperationFrontDesk_idx` (`FrontDeskID`),
  CONSTRAINT `OperationFrontDesk` FOREIGN KEY (`FrontDeskID`) REFERENCES `frontdesk_tbl` (`FrontDeskID`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `operation_tbl`
--

LOCK TABLES `operation_tbl` WRITE;
/*!40000 ALTER TABLE `operation_tbl` DISABLE KEYS */;
INSERT INTO `operation_tbl` VALUES ('BT11','Blood Test','2025-03-08','A, B, O',1,500.00,'00:30:00'),('CE43','X-RAY','2021-11-25','AAA',1,5000.00,'01:30:00'),('CK10','Checkup','2024-12-12','asda',1,500.00,'01:00:00'),('EYE5','Eye Checkup','2025-03-08','dfgdfg',2,5000.00,'01:30:00'),('XE101','Surgery','2020-12-21','VVV',2,1000.00,'01:50:00');
/*!40000 ALTER TABLE `operation_tbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patient_tbl`
--

DROP TABLE IF EXISTS `patient_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patient_tbl` (
  `PatientID` bigint NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(45) NOT NULL,
  `MiddleName` varchar(45) NOT NULL,
  `LastName` varchar(45) NOT NULL,
  `Age` int NOT NULL,
  `Gender` varchar(45) NOT NULL,
  `ContactNumber` varchar(45) DEFAULT NULL,
  `BirthDate` date NOT NULL,
  `Address` varchar(100) NOT NULL,
  PRIMARY KEY (`PatientID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patient_tbl`
--

LOCK TABLES `patient_tbl` WRITE;
/*!40000 ALTER TABLE `patient_tbl` DISABLE KEYS */;
/*!40000 ALTER TABLE `patient_tbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patientoperation_tbl`
--

DROP TABLE IF EXISTS `patientoperation_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patientoperation_tbl` (
  `PatientOperationNo` bigint NOT NULL AUTO_INCREMENT,
  `PatientID` bigint NOT NULL,
  `RoomNo` bigint NOT NULL,
  `Bill` decimal(10,2) NOT NULL,
  PRIMARY KEY (`PatientOperationNo`),
  KEY `PatientOperation2_idx` (`PatientID`),
  KEY `PatientOperation2_idx1` (`RoomNo`),
  CONSTRAINT `PatientOperation3` FOREIGN KEY (`RoomNo`) REFERENCES `rooms_tbl` (`RoomNo`),
  CONSTRAINT `PatientOperation4` FOREIGN KEY (`PatientID`) REFERENCES `patient_tbl` (`PatientID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patientoperation_tbl`
--

LOCK TABLES `patientoperation_tbl` WRITE;
/*!40000 ALTER TABLE `patientoperation_tbl` DISABLE KEYS */;
/*!40000 ALTER TABLE `patientoperation_tbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rooms_tbl`
--

DROP TABLE IF EXISTS `rooms_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rooms_tbl` (
  `RoomNo` bigint NOT NULL,
  `Occupation` varchar(45) NOT NULL DEFAULT 'Not Occupied',
  PRIMARY KEY (`RoomNo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rooms_tbl`
--

LOCK TABLES `rooms_tbl` WRITE;
/*!40000 ALTER TABLE `rooms_tbl` DISABLE KEYS */;
INSERT INTO `rooms_tbl` VALUES (401,''),(402,''),(403,''),(404,''),(405,''),(406,''),(407,''),(408,''),(409,''),(410,''),(411,''),(412,'');
/*!40000 ALTER TABLE `rooms_tbl` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-03-10 14:50:09
