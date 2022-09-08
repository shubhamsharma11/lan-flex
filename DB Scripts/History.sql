CREATE TABLE `lanflex`.`history` (
  `UploadedFileId` BIGINT NOT NULL AUTO_INCREMENT,
  `FileId` BIGINT NOT NULL,
  `UploadedTime` DATETIME NOT NULL,
  `Historycol` VARCHAR(45) NULL,
  PRIMARY KEY (`UploadedFileId`),
  UNIQUE INDEX `UploadedFileId_UNIQUE` (`UploadedFileId` ASC) VISIBLE,
  INDEX `fileId_history_fileId_filesDetails_idx` (`FileId` ASC) VISIBLE,
  CONSTRAINT `fileId_history_fileId_filesDetails`
    FOREIGN KEY (`FileId`)
    REFERENCES `lanflex`.`filesdetails` (`FileId`)
    ON DELETE RESTRICT
    ON UPDATE NO ACTION);
