CREATE TABLE `lanflex`.`filesdetails` (
  `FileId` BIGINT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  `Details` VARCHAR(255) NULL,
  `ThumbPath` VARCHAR(255) NULL,
  `Extension` VARCHAR(255) NOT NULL,
  `FilePath` VARCHAR(255) NOT NULL,
  `Type` VARCHAR(45) NOT NULL,
  `LastUpdatedAt` DATETIME NOT NULL,
  `CreatedAt` DATETIME NOT NULL,
  `FilesDetailscol` VARCHAR(45) NULL,
  PRIMARY KEY (`FileId`),
  UNIQUE INDEX `FileId_UNIQUE` (`FileId` ASC) VISIBLE);
