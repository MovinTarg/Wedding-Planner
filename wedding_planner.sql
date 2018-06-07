-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema wedding_planner
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema wedding_planner
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `wedding_planner` DEFAULT CHARACTER SET utf8 ;
USE `wedding_planner` ;

-- -----------------------------------------------------
-- Table `wedding_planner`.`User`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wedding_planner`.`User` (
  `UserId` INT NOT NULL AUTO_INCREMENT,
  `FirstName` VARCHAR(255) NULL,
  `LastName` VARCHAR(255) NULL,
  `Email` VARCHAR(255) NULL,
  `Password` VARCHAR(255) NULL,
  `CreatedAt` DATETIME NULL,
  `UpdatedAt` DATETIME NULL,
  PRIMARY KEY (`UserId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `wedding_planner`.`Wedding`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wedding_planner`.`Wedding` (
  `WeddingId` INT NOT NULL AUTO_INCREMENT,
  `WedderOne` VARCHAR(255) NULL,
  `WedderTwo` VARCHAR(255) NULL,
  `WeddingDate` DATETIME NULL,
  `WeddingAddress` VARCHAR(255) NULL,
  `CreatedAt` DATETIME NULL,
  `UpdatedAt` DATETIME NULL,
  `UserId` INT NOT NULL,
  PRIMARY KEY (`WeddingId`),
  INDEX `fk_Wedding_User_idx` (`UserId` ASC),
  CONSTRAINT `fk_Wedding_User`
    FOREIGN KEY (`UserId`)
    REFERENCES `wedding_planner`.`User` (`UserId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `wedding_planner`.`Guest`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wedding_planner`.`Guest` (
  `GuestId` INT NOT NULL AUTO_INCREMENT,
  `UserId` INT NOT NULL,
  `WeddingId` INT NOT NULL,
  PRIMARY KEY (`GuestId`),
  INDEX `fk_Guest_User1_idx` (`UserId` ASC),
  INDEX `fk_Guest_Wedding1_idx` (`WeddingId` ASC),
  CONSTRAINT `fk_Guest_User1`
    FOREIGN KEY (`UserId`)
    REFERENCES `wedding_planner`.`User` (`UserId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Guest_Wedding1`
    FOREIGN KEY (`WeddingId`)
    REFERENCES `wedding_planner`.`Wedding` (`WeddingId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
